using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Door : MonoBehaviour
{
    public bool locked;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
