using System;
using UnityEngine;

public class ChangeRoation : MonoBehaviour
{
    public Vector3 newRotation;
    public bool changetype;
    
    public void ChangeRotationEvent()
    {
        if (changetype)
        {
            transform.eulerAngles = newRotation;
        }
        else
        {
            transform.Rotate(newRotation);
        }
    }
}