using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform head;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Quaternion rot = new Quaternion(0,head.rotation.y,0,head.rotation.w);
        transform.rotation = rot;
    }
}
