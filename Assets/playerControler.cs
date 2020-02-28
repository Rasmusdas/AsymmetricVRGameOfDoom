using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class playerControler : MonoBehaviour
{
    public SteamVR_Action_Vector2 input;
    public float speed = 1;

    // Start is called before the first frame update
    void Start()
    {
        transform.position += speed * Time.deltaTime * new Vector3(input.axis.x,0, input.axis.y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
