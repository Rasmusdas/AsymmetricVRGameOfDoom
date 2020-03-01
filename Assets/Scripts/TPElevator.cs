using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TPElevator : MonoBehaviour
{
    public Transform tpTarget;

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            col.gameObject.transform.position = tpTarget.position;
        }
    }

}
