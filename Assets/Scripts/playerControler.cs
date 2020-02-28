using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class playerControler : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean sprint;
    public float speed = 2;
    public Transform head;

    void Update()
    {
        float x = input.axis.x * speed * Time.deltaTime;
        float y = input.axis.y * speed * Time.deltaTime;
        Vector3 movementVector = head.forward * y + head.right * x;
        transform.Translate(movementVector.x,0,movementVector.z);
    }
}
