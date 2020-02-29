using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;
using Valve.VR;

public class HandAnim : MonoBehaviour
{
    public SteamVR_Action_Boolean grab;
    public Animator ainm;
    public Transform tras;
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = tras.localPosition;
        transform.localRotation = tras.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        ainm.SetBool("garb", grab.GetState(SteamVR_Input_Sources.Any));
        ainm.SetBool("Garb", grab.GetState(SteamVR_Input_Sources.Any));
    }
}
