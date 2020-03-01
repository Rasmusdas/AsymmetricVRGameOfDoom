using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConsoleScreen : MonoBehaviour
{
    public string[] lines;

    private void Start()
    {
        lines = new string[transform.childCount];
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<Text>().text = lines[i];
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
