using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cameras : MonoBehaviour
{
    public static Dictionary<string, Camera> cameraList = new Dictionary<string, Camera>();
    public static Dictionary<string, Camera> doorList = new Dictionary<string, Camera>();

    public Camera currentCam;
    public RenderTexture roofCamera;

    [SerializeField]
    private Camera[] cameras;

    private void Awake()
    {
        currentCam = Camera.current;
        foreach(Camera cam in cameras)
        {
            AddCamera(cam);
        }
    }

    public static void AddCamera(Camera cam)
    {
        if(!cameraList.ContainsKey(cam.name.ToLower()))
        {
            cameraList.Add(cam.name.ToLower(), cam);
        }
    }
    public static void AddDoor(GameObject door)
    {

    }

    public void SwapCamera(Camera cam)
    {
        if(cameraList.ContainsKey(cam.name.ToLower()))
        {
            currentCam = cameraList[cam.name.ToLower()];
            currentCam.targetTexture = roofCamera;
        }
    }

    public void HandleCommand(string s)
    {
        s = s.ToLower();
        string command = s.Split('.')[0];
        string input = s.Split('.')[1];
        switch (command)
        {
            case "hack":
                if (cameraList.ContainsKey(input))
                {
                    SwapCamera(cameraList[input]);
                }
                break;
            case "open":
                break;
            default:
                break;
        }
    }
}
