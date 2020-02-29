using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : MonoBehaviour
{
    public float movedAmount;
    Vector3 start;
    Rigidbody rb;
    Vector3 wheelStart;
    public Door door;
    public Vector3 rotationEnd;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        start = door.transform.localPosition;
        wheelStart = transform.localPosition;
    }
    public void Update()
    {
        Debug.Log(door.transform.localEulerAngles);
        movedAmount += rb.angularVelocity.z;
        if(movedAmount < 0)
        {
            movedAmount = 0;
        }
        if(movedAmount > 150)
        {
            CommandHandler.vaultOpen = true;
            rb.constraints = RigidbodyConstraints.FreezeAll;
            rb.centerOfMass = door.GetComponent<Rigidbody>().centerOfMass;
            if(transform.localEulerAngles.magnitude != 0)
            {
                transform.localEulerAngles = Vector3.zero;
                transform.localPosition = wheelStart;
            }
        }
        if(door.transform.localEulerAngles.y-350 > 0)
        {
            door.transform.localEulerAngles = Vector3.zero;
            door.transform.localPosition = start;
        }
    }
}
