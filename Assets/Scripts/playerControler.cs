using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class playerControler : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public SteamVR_Action_Boolean sprint;
    public float speed = 2;
    private float currentSpeed;
    public Transform head;
    CharacterController charCon;

    private void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        currentSpeed = speed;
        if(sprint.active)
        {
            currentSpeed += speed;
        }
        Vector3 dir = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x,0,input.axis.y));
        charCon.Move(Time.deltaTime*speed*Vector3.ProjectOnPlane(dir,Vector3.up));
    }
}
