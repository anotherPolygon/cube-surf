using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommonValues : MonoBehaviour
{
    public bool isTurningLeft;
    public bool isTurnedLeft;

    public bool isTurningRight;
    public bool isTurnedRight;

    public void SetIsTurned(bool is_left_sail, bool value)
    {
        if (is_left_sail)
        {
            isTurnedLeft = value;
        }
        else
        {
            isTurnedRight = value;
        }
    }
    public void SetIsTurning(bool is_left_sail, bool value)
    {
        if (is_left_sail)
        {
            isTurningLeft = value;
        }
        else
        {
            isTurningRight = value;
        }
    }
    public bool GetIsTurning(bool is_left_sail)
    {
        if (is_left_sail)
        {
            return isTurningLeft;
        }
        else
        {
            return isTurningRight;
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
