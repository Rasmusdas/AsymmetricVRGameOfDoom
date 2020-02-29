using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonVRPlayerMovement : MonoBehaviour
{

    private float currentSpeed;
    public float movementSpeed;

    [SerializeField]
    private float mouseSensitivity;

    [SerializeField]
    private Transform playerCamera;

    [SerializeField]
    private GameObject goodJob;
    
    private bool pickedUp;

    [SerializeField]
    private Material shineShader;
    private Transform pickedUpItem;
    private Transform pickupHand;
    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {

        // Player Movement
        float x = Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * movementSpeed * Time.deltaTime;
        Vector3 movementVector = transform.forward * y + transform.right*x;
        rb.velocity = new Vector3(movementVector.x,rb.velocity.y,movementVector.z);
    }
}
