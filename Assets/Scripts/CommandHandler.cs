using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    public static Dictionary<string, Camera> cameraList = new Dictionary<string, Camera>();
    public static Dictionary<string, Door> doorList = new Dictionary<string, Door>();
    public static Dictionary<string, GameObject> lightList = new Dictionary<string, GameObject>();

    public Camera currentCam;
    public RenderTexture roofCamera;
    public ConsoleScreen console;

    public float lightMeter;
    public float lightTimer;

    private bool lights;

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

    public static void AddDoor(Door door)
    {
        if (!doorList.ContainsKey(door.name.ToLower()))
        {
            doorList.Add(door.name.ToLower(), door);
        }
    }

    public static void AddLight(GameObject light)
    {
        if (!lightList.ContainsKey(light.name.ToLower()))
        {
            lightList.Add(light.name.ToLower(), light);
        }
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

    public void ChangeLightState(GameObject lightObject)
    {
        Light light = lightObject.GetComponent<Light>();
        if (lightList.ContainsKey(light.name.ToLower()))
        {
            light.GetComponent<Light>().enabled = false;
            StartCoroutine(RelightLights(light));
        }   
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
                    console.WriteLine(string.Format("\"{0}\" could not be found or is unavailable", input));
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
                    console.WriteLine(string.Format("\"{0}\" could not be found or is unavailable",input));
                }
                else
                {
                    console.WriteLine("Missing argument. Correct syntax: open.[arg]");
                }
                break;
            case "turnoff":
                if (lightList.ContainsKey(input))
                {
                    console.WriteLine(string.Format("Turned off \"{0}\"", input));
                    ChangeLightState(lightList[input]);
                }
                else if(input.Length != 0)
                {
                    console.WriteLine(string.Format("\"{0}\" could not be found or is unavailable", input));
                }
                else
                {
                    console.WriteLine("Missing argument, Correct syntax: turnoff.[arg]");
                }
                break;
            default:
                console.WriteLine(string.Format("\"{0}\" is not a recognized command",s));
                break;
        }
    }

    IEnumerator RelightLights(Light light)
    {
        yield return new WaitForSeconds(lightTimer);
        light.enabled = true;
    }
}
