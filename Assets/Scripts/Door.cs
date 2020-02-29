using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour
{
    public bool locked;
    protected Rigidbody rb;
    Vector3 startAngle;
    public Vector3 pivot;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = rb.centerOfMass - pivot;
        startAngle = transform.eulerAngles;
    }

    public virtual void Update()
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
