using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class VaultDoor : MonoBehaviour
{
    public float movedAmount;
    Vector3 start;
    Rigidbody rb;
    Vector3 wheelStart;
    public Door door;
    public Vector3 rotationEnd;
    public TextMesh clipboard;
    public Interactable interactable;

    private void Awake()
    {
        name = "vault" + Random.Range(1000, 9999);    
    }

    private void Start()
    {
        clipboard.text = "vault\n" + name.Substring(5); 
        rb = GetComponent<Rigidbody>();
        start = door.transform.localPosition;
        wheelStart = transform.localPosition;
    }
    public void Update()
    {
        if(!CommandHandler.overrideDone)
        {
            movedAmount = 0;
        }
        Debug.Log(door.transform.localEulerAngles);
        movedAmount += rb.angularVelocity.z;
        if (movedAmount < 0)
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
