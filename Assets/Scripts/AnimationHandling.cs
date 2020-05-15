using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandling : MonoBehaviour
{

    public GameObject pole;
    public GameObject sail;

    private Animator m_pole_animator;
    private Animator m_sail_animator;
    private Collider m_pole_collider;
    private Collider m_sail_collider;
    public bool is_left_sail;

    public CommonValues commonValues;

    private void Start()
    {
        commonValues.SetIsTurning(is_left_sail, false);

        m_pole_animator = pole.GetComponent<Animator>();
        m_sail_animator = sail.GetComponent<Animator>();
        m_pole_collider = pole.GetComponent<Collider>();
        m_sail_collider = sail.GetComponent<Collider>();

        m_pole_collider.enabled = false;
        m_sail_collider.enabled = false;


    }

void StartTurning()
    {
        commonValues.SetIsTurning(is_left_sail, true);
    }

    void IsTurned()
    {

    }

    void StopTurning()
    {

    }

    private AnimatorStateInfo GetCurrentAnimatorState(Animator animator)
    {
        return animator.GetCurrentAnimatorStateInfo(0);
    }

    private bool IsAnimationInState(Animator animator, string state)
    {
        return (GetCurrentAnimatorState(animator).IsName(state) & !animator.IsInTransition(0));
    }

    private bool IsPoleRetracted()
    {
        return IsAnimationInState(m_pole_animator, "Retracted");
    }

    private bool IsPoleExtracted()
    {
        return IsAnimationInState(m_pole_animator, "Extracted");
    }

    private bool IsSailClosed()
    {
        return IsAnimationInState(m_sail_animator, "SailClose");
    }

    private bool IsSailOpened()
    {
        return IsAnimationInState(m_sail_animator, "SailOpen");
    }

    private void HandleAnimation()
    {
        if (commonValues.GetIsTurning(is_left_sail))
        {
            if (IsPoleRetracted())
            {
                m_pole_animator.SetTrigger("Extract");
            }
            else if (IsSailClosed() && IsPoleExtracted())
            {
                m_sail_animator.SetTrigger("Open");
                commonValues.SetIsTurned(is_left_sail, true);
            }

        } else {
            if (IsSailOpened())
            {
                m_sail_animator.SetTrigger("Close");
            }
            if (IsPoleExtracted() && IsSailClosed())
            {
                m_pole_animator.SetTrigger("Retract");
                commonValues.SetIsTurned(is_left_sail, false);
            }
        }
    }

    private void FixedUpdate()
    {
        HandleAnimation();
    }
}
