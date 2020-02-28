using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreen : MonoBehaviour
{
    public string[] lines = new string [5];

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (lines.Length > i)
            {
                transform.GetChild(i).GetComponent<Text>().text = lines[i];
            }
        }
    }

    public void WriteLine(string line)
    {
        for (int i = lines.Length-1; i > 0; i--)
        {
            lines[i] = lines[i - 1];
        }
        lines[0] = line;
    }
}
