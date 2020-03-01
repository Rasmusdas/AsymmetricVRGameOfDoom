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
    private Vector3 dir;

    private void Start()
    {
        charCon = GetComponent<CharacterController>();
    }

    void Update()
    {
        Debug.Log(LevelManager.LastLevel);
        currentSpeed = speed;
        if(sprint.active)
        {
            currentSpeed += speed;
        }
        else
        {
            currentSpeed = speed;
        
        }
        dir = Player.instance.hmdTransform.TransformDirection(new Vector3(input.axis.x, 0, input.axis.y));
    }
    private void FixedUpdate()
    {
        charCon.Move(Time.deltaTime * currentSpeed * Vector3.ProjectOnPlane(dir, Vector3.up) - new Vector3(0, 9.81f, 0) * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Elevator")
        {
            GetComponent<CharacterController>().enabled = false;
            transform.position = hit.gameObject.GetComponent<TPElevator>().tpTarget.position;
            GetComponent<CharacterController>().enabled = true;
        }
    }
}
