using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOutput : MonoBehaviour
{
    public GameObject cameraScreenBlocker;
    public GameObject floorplanScreenBlocker;

    public void ToggleScreen(string screen)
    {
        if (screen == "floor")
        {
            floorplanScreenBlocker.SetActive(!floorplanScreenBlocker.activeInHierarchy);
        }
        else if (screen == "camera")
        {
            cameraScreenBlocker.SetActive(!cameraScreenBlocker.activeInHierarchy);
        }
    }
}
