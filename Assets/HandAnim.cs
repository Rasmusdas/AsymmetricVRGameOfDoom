using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandAnim : MonoBehaviour
{

    public Animator ainm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("Grab Grip"))
        {
            ainm.SetBool("grab", true);
        }
        else
        {
            ainm.SetBool("grab", false);
        }
    }
}
