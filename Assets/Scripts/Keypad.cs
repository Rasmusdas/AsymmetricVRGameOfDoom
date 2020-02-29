using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    public TextMesh text;
    public Door door;

    void Start()
    {
        if(door != null)
        {
            text.text = door.name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
