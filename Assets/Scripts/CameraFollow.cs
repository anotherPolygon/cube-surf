using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform objectToFollow;
    public Vector3 offset;
    public float followSpeed = 10f;
    public float turnSpeed = 10f;

    private void LookAtTarget()
    {
        Vector3 _forwardDirection = objectToFollow.position - transform.position;
        Quaternion _rotation = Quaternion.LookRotation(_forwardDirection, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, turnSpeed * Time.deltaTime);
    }

    private void MoveToTarget()
    {
        Vector3 _targetPos = objectToFollow.position +
                             objectToFollow.forward * offset.z +
                             objectToFollow.right * offset.x +
                             objectToFollow.up * offset.y;
        transform.position = Vector3.Lerp(transform.position, _targetPos, followSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        LookAtTarget();
        MoveToTarget();
    }
}
