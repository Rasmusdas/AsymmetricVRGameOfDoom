using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TipController : MonoBehaviour
{
    public GameObject tipPosition;
    public List<Tip> tipList;
    private int currentTip;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public class Tip
{
    public string Hedder { get; set; }
    public string Tips { get; set; }

    public Tip(string hedder, string tips)
    {
        Hedder = hedder;
        Tips = tips;
    }
}
