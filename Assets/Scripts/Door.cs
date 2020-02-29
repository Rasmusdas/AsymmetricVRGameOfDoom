using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour
{
    public bool locked;
    Rigidbody rb;
    Vector3 startAngle;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rb.centerOfMass - new Vector3(0, 0, 1f);
        startAngle = transform.eulerAngles;
    }

    private void Update()
    {
        if (locked)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
        else
        {
            rb.constraints = (RigidbodyConstraints)94;
        }
    }
}
