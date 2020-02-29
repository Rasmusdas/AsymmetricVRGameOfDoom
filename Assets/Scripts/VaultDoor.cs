using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    public float movedAmount;
    Vector3 start;
    Rigidbody rb;
    public Door door;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        start = door.transform.localPosition;
    }
    public void Update()
    {
        movedAmount += rb.angularVelocity.z;
        if(movedAmount < 0)
        {
            movedAmount = 0;
        }
        if(movedAmount > 5)
        {
            door.locked = false;
            rb.constraints = RigidbodyConstraints.FreezePosition;
        }
        if(door.transform.localEulerAngles.y-300 > 0)
        {
            door.transform.localEulerAngles = Vector3.zero;
            door.transform.localPosition = start;
        }
    }
}
