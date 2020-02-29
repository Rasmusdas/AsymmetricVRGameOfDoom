﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipController : MonoBehaviour
{
    public GameObject tipPosition;
    public List<Tip> tipList = new List<Tip>();
    private int currentTip = 0;
    Text hedderText;
    Text tipText;

    // Start is called before the first frame update
    void Start()
    {
        hedderText = tipPosition.transform.GetChild(0).GetComponent<Text>();
        tipText = tipPosition.transform.GetChild(1).GetComponent<Text>();
        updateText();
    }  

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            MoveUp();
        }
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            MoveDown();
        }
    }

    public void MoveUp()
    {
        if (currentTip < tipList.Count - 1)
        {
            currentTip++;
        }
        else
        {
            currentTip = 0;
        }
        updateText();
    }

    public void MoveDown()
    {
        if (currentTip > 0)
        {
            currentTip--;
        }
        else
        {
            currentTip = tipList.Count - 1;
        }
        updateText();
    }

    void updateText()
    {
        hedderText.text = tipList[currentTip].header;
        string temp = "";
        for (int i = 0; i < tipList[currentTip].tips.Count; i++)
        {
            temp += i+1 + ". " + tipList[currentTip].tips[i] + "\n";
        }
        tipText.text = temp;
    }
}

[Serializable]
public class Tip
{
    public string header;
    public List<string> tips;
}
