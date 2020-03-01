using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandHandler : MonoBehaviour
{
    public static Dictionary<string, GameObject> cameraList = new Dictionary<string, GameObject>();
    public static Dictionary<string, Door> doorList = new Dictionary<string, Door>();
    public static Dictionary<string, GameObject> lightList = new Dictionary<string, GameObject>();

    public GameObject currentCam;
    public RenderTexture roofCamera;
    public ConsoleScreen console;

    public float lightMeter;
    public float lightTimer;

    public VaultDoor vaultdoor;
    public static bool vaultOpen;
    public static bool overrideDone;

    [SerializeField]
    private GameObject[] cameras;

    [SerializeField]
    private Door[] doors;

    [SerializeField]
    private GameObject[] lights;

    private void Start()
    {
        doorList = new Dictionary<string, Door>();
        cameraList = new Dictionary<string, GameObject>();
        lightList = new Dictionary<string, GameObject>();
        cameras = GameObject.FindGameObjectsWithTag("SurvCam");
        GameObject[] tempDoors = GameObject.FindGameObjectsWithTag("Door");
        doors = new Door[tempDoors.Length];
        for (int i = 0; i < doors.Length; i++)
        {
            doors[i] = tempDoors[i].GetComponent<Door>();
        }
        lights = GameObject.FindGameObjectsWithTag("GameLights");
        List<int> taken = new List<int>();
        foreach (GameObject cam in cameras)
        {
            int random = Random.Range(0, 99);
            while (taken.Contains(random))
            {
                random = Random.Range(0, 99);
            }
            taken.Add(random);
            if (random < 10)
            {
                cam.name = "0" + random;
            }
            else
            {
                cam.name = random.ToString();
            }
            AddCamera(cam);
        }
        foreach (Door door in doors)
        {
            if(door.name == "exit")
            {
                continue;
            }
            int random = Random.Range(0, 99);
            while (taken.Contains(random))
            {
                random = Random.Range(0, 99);
            }
            taken.Add(random);
            if (random < 10)
            {
                door.name = "0" + random;
            }
            else
            {
                door.name = random.ToString();
            }
            AddDoor(door);
        }
        foreach (GameObject light in lights)
        {
            int random = Random.Range(0, 99);
            while (taken.Contains(random))
            {
                random = Random.Range(0, 99);
            }
            taken.Add(random);
            if (random < 10)
            {
                light.name = "0" + random;
            }
            else
            {
                light.name = random.ToString();
            }
            AddLight(light);
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

    public static void AddCamera(GameObject cam)
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

    public void SwapCamera(GameObject cam)
    {
        if(cameraList.ContainsKey(cam.name.ToLower()))
        {
            currentCam = cameraList[cam.name.ToLower()].GetComponent<SurveillanceCamera>().gameObject;
            currentCam.GetComponent<SurveillanceCamera>().survCam.targetTexture = roofCamera;
            currentCam.GetComponent<AudioSource>().Play();
        }
    }

    public IEnumerator LockDoor(Door door)
    {
        door.locked = true;
        while (!(Mathf.Abs(door.transform.localEulerAngles.y - door.startAngle.y) < 0.1f))
        {
            door.transform.localRotation = Quaternion.RotateTowards(door.transform.localRotation, Quaternion.Euler(door.startAngle), 3);
            yield return new WaitForEndOfFrame();
        }
        door.transform.GetComponent<AudioSource>().Play();
    }

    public IEnumerator UnlockDoor(Door door)
    {
        door.locked = false;
        door.transform.GetComponent<AudioSource>().Play();
        while (!(Mathf.Abs(door.transform.localEulerAngles.y - door.openAngle.y) < 0.1f))
        {
            door.transform.localRotation = Quaternion.RotateTowards(door.transform.localRotation, Quaternion.Euler(door.openAngle), 3);
            yield return new WaitForEndOfFrame();
        }
    }

    public void ChangeLightState(GameObject lightObject)
    {
        for (int i = 0; i < lightObject.transform.childCount; i++)
        {
            if(lightObject.transform.GetChild(i).TryGetComponent(out Light l))
            {
                l.enabled = false;
                StartCoroutine(RelightLights(l));
            }
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
                if(input == "vaultdoor")
                {
                    if(vaultOpen && overrideDone)
                    {
                        StartCoroutine(OpenVaultDoor());
                        console.WriteLine("Vault door opened");
                    }
                    else
                    {
                        console.WriteLine(string.Format("Vault door is locked"));
                    }
                }
                else if(doorList.ContainsKey(input))
                {
                    console.WriteLine(string.Format("Unlocked door \"{0}\"",input));
                    StartCoroutine(UnlockDoor(doorList[input]));
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
            case "close":
                if (doorList.ContainsKey(input))
                {
                    console.WriteLine(string.Format("Locked door \"{0}\"", input));
                    StartCoroutine(LockDoor(doorList[input]));
                }
                else if (input.Length != 0)
                {
                    console.WriteLine(string.Format("\"{0}\" could not be found or is unavailable", input));
                }
                else
                {
                    console.WriteLine("Missing argument. Correct syntax: close.[arg]");
                }
                break;
            case "turnoff":
                if (lightList.ContainsKey(input))
                {
                    console.WriteLine(string.Format("Switched off \"{0}\"", input));
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
            case "override":
                if(vaultdoor.name == input)
                {
                    overrideDone = true;
                    console.WriteLine(string.Format("Security override complete."));
                    console.WriteLine("Turn the Vault Door wheel clockwise until it stops");
                    console.WriteLine("then type open.vaultdoor to open the vault door");
                }
                else if(input.Length != 0)
                {
                    console.WriteLine(string.Format("Could not override \"{0}\"", input));
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

    IEnumerator OpenVaultDoor()
    {
        yield return new WaitForEndOfFrame();
        vaultdoor.door.transform.localRotation = Quaternion.RotateTowards(vaultdoor.door.transform.localRotation, Quaternion.Euler(vaultdoor.rotationEnd), 1f);
        StartCoroutine(OpenVaultDoor());
    }
}
