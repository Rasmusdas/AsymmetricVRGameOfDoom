using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurveillanceCamera : MonoBehaviour
{
    public float maxDeltaAngle = 45f;
    public float currentChange = 0;
    public float angleChange = 0.1f;
    public Vector3 angle;
    public TextMesh text;
    public Camera survCam;

    private void Start()
    {
        angle = transform.rotation.eulerAngles;
        text.text = name;
    }

    public void ChangeRotation(bool left)
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(currentChange > -maxDeltaAngle)
            {
                currentChange -= 0.2f;
                transform.Rotate(0, -0.2f, 0, Space.World);
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (currentChange < maxDeltaAngle) 
            {
                currentChange += 0.2f;
                transform.Rotate(0, 0.2f, 0, Space.World);
            }
        }
    }
}
