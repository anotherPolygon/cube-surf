using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CommonValues commonValues;

    public float forwardSpeed;
    public float sidewaySpeed;

    public float accelerationSpeed = 5;
    public float maxAcceleration = 30;
    public float maxSpeed = 100;
    public float changesDuration = 1;

    private Rigidbody playerBody;
    private Material playerMaterial;
    private Renderer playerRenderer;
    private Renderer[] childRenderers;

    //private GameObject leftSail;
    //private GameObject rightSail;

    private float m_horizontalInput;
    private float m_verticalInput;
    private float m_currentAcceleration;
    private float m_currentSpeed;

    private int m_touchingContactsMin = 2;
    private bool m_isTouchingGround;    
    private bool m_isTouchingObstacle;
    private bool m_canAccelerate;
    private bool m_isTryingToAccelerate;

    private bool m_isSteering;

    private void GetInput()
    {
        m_horizontalInput = Input.GetAxis("Horizontal");
        m_verticalInput = Input.GetAxis("Vertical");
    }

    private void HandleState()
    {
        m_canAccelerate = m_isTouchingGround | m_isTouchingObstacle;
        m_isTryingToAccelerate = Input.GetAxis("Vertical") != 0;

        HandleColor();
    }

    private void HandleColor() { 

        foreach (Renderer _childRenderer in childRenderers)
        {
            if (m_canAccelerate) {
                _childRenderer.material.color = Color.Lerp(_childRenderer.material.color, Color.green, Time.deltaTime / changesDuration);
            } else {
                _childRenderer.material.color = Color.Lerp(_childRenderer.material.color, Color.red, Time.deltaTime / changesDuration);
            }
        
        }
    }

    private void LerpColor(Color current, Color to)
    {
        Color.Lerp(current, to, Time.deltaTime / changesDuration);
    }

    private Animator getGameObjectAnimator(GameObject gameObject)
    {
        return gameObject.GetComponent<Animator>();
    }

    private void Steer()
    {
        float portionOfTurnSpeed;

        commonValues.isTurningRight = m_horizontalInput > 0;
        commonValues.isTurningLeft = m_horizontalInput < 0;
        if (m_canAccelerate && (commonValues.isTurnedLeft || commonValues.isTurnedRight)) {
            portionOfTurnSpeed = m_currentSpeed / maxSpeed;
            transform.Rotate(Vector3.up, m_horizontalInput * sidewaySpeed * portionOfTurnSpeed * Time.deltaTime);
        }
    }

    private void Accelerate()
    {
        if (m_canAccelerate)
        {
            m_currentAcceleration = m_verticalInput * accelerationSpeed;
        } else {
            m_currentAcceleration = 0;
        }

        if (m_canAccelerate)
        {
            m_currentSpeed += m_currentAcceleration;
        }
        else
        {
            m_currentSpeed = 0;
        }
        
        m_currentSpeed = Mathf.Clamp(m_currentSpeed, -maxSpeed, maxSpeed);
    }

    private void Move()
    {
        if (m_canAccelerate & m_isTryingToAccelerate) {
            playerBody.velocity += playerBody.transform.forward * m_currentSpeed * Time.deltaTime;
        } 
    }

    private void Start()
    {
        playerBody = GetComponent<Rigidbody>();
        playerMaterial = GetComponent<Material>();
        playerRenderer = GetComponent<Renderer>();
        childRenderers = GetComponentsInChildren<Renderer>();

        //leftSail = GameObject.Find("LeftSail");
        //rightSail = GameObject.Find("RightSail");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {
            if (collision.contactCount > m_touchingContactsMin) {
                m_isTouchingGround = true;
            }
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            if (collision.contactCount > m_touchingContactsMin)
            {
                m_isTouchingObstacle = true;
            }
        }
    }

        private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground") {
            m_isTouchingGround = false;
        }
        if (collision.gameObject.tag == "Obstacle")
        {
            m_isTouchingObstacle = false;
        }
    }

    private void FixedUpdate()
    {
        GetInput();
        Debug.Log(m_horizontalInput);
        HandleState();
        Steer();
        Accelerate();
        Move();
    }
}
