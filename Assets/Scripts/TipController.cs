using System;
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

    public void MoveDown()
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

    public void MoveUp()
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
            int previusTipsCount = 0;
            if (tipList[currentTip].number != 0)
            {   
                for (int j = 0; j < tipList.Count; j++)
                {
                    if (tipList[j].header == tipList[currentTip].header && tipList[j] != tipList[currentTip])
                    {
                        previusTipsCount += tipList[currentTip].tips.Count;
                    }
                }
            }
            temp += (i+1+previusTipsCount) + ". " + tipList[currentTip].tips[i] + "\n";
        }
        tipText.text = temp;
    }
}

[Serializable]
public class Tip
{
    public string header;
    public int number = 0;
    public List<string> tips;
}
