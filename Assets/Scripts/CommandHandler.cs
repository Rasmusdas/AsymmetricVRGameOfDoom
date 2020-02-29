using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    public static Dictionary<string, Camera> cameraList = new Dictionary<string, Camera>();
    public static Dictionary<string, Door> doorList = new Dictionary<string, Door>();

    public Camera currentCam;
    public RenderTexture roofCamera;
    public ConsoleScreen console;

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

    private void Update()
    {
        // Rotation of Cameras
        if(currentCam != null)
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                currentCam.gameObject.GetComponent<SurveillanceCamera>().ChangeRotation(true);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                currentCam.gameObject.GetComponent<SurveillanceCamera>().ChangeRotation(false);
            }
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

    public void UnlockDoor(Door door)
    {
        door.locked = false;
    }

    public void HandleCommand(string s)
    {
        if (s.Length == 0) return;
        s = s.ToLower();
        string command = s;
        string input = "";
        if (s.Contains("."))
        {
            command = s.Split('.')[0];
            input = s.Split('.')[1];
        }
        switch (command)
        {
            case "hack":
                if (cameraList.ContainsKey(input))
                {
                    SwapCamera(cameraList[input]);
                    console.WriteLine(string.Format("Connecting to \"{0}\"",input));
                }
                else if(input.Length != 0)
                {
                    console.WriteLine(string.Format("Camera \"{0}\" could not be found or is unavailable", input));
                }
                else
                {
                    console.WriteLine("Missing argument, Correct syntax: hack.[arg]");
                }
                break;
            case "open":
                if(doorList.ContainsKey(input))
                {
                    console.WriteLine(string.Format("Unlocked door \"{0}\"",input));
                    UnlockDoor(doorList[input]);
                }
                else if(input.Length != 0)
                {
                    console.WriteLine(string.Format("Door \"{0}\" could not be found or is unavailable",input));
                }
                else
                {
                    console.WriteLine("Missing argument. Correct syntax: open.[arg]");
                }
                break;
            default:
                console.WriteLine(string.Format("\"{0}\" is not a recognized command",s));
                break;
        }
    }
}
