using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class GuardAI : MonoBehaviour
{
    public Vector3 currentTarget;
    public Vector3 lastTargetPosition;
    public Transform player;

    public bool lightsOff;

    public Vector3 startPosition;

    public NavMeshAgent agent;

    public float spottingRange;

    private void Start()
    {
        startPosition = transform.position;
        lastTargetPosition = startPosition;
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        foreach(GameObject g in CommandHandler.lightList.Values)
        {
            for (int i = 0; i < g.transform.childCount; i++)
            {
                if (g.transform.GetChild(i).TryGetComponent(out Light l))
                {
                    if(l.enabled)
                    {
                        if (!Physics.Raycast(player.position, g.transform.GetChild(i).position, out RaycastHit lightHit, Vector3.Distance(player.position, g.transform.GetChild(i).position)))
                        {
                            lightsOff = false;
                            break;
                        }
                        else
                        {
                            lightsOff = true;
                        }
                    }
                }
            }
            if(!lightsOff)
            {
                break;
            }
        }
        if(!lightsOff)
        {
            if (Physics.Raycast(transform.position, player.position+Vector3.up/2 - transform.position, out RaycastHit hit, spottingRange))
            {
                if (hit.transform.tag == "Player")
                {
                    currentTarget = hit.transform.position;
                    lastTargetPosition = hit.transform.position;
                    agent.destination = currentTarget;
                    return;
                }
            }
            if (Vector3.Distance(lastTargetPosition, transform.position) > 1f)
            {
                currentTarget = lastTargetPosition;
            }
            else
            {
                lastTargetPosition = startPosition;
                currentTarget = startPosition;
            }
            agent.destination = currentTarget;
        }
        else
        {
            agent.ResetPath();
        }
    }

    GameObject GetClosestLight()
    {
        GameObject closestObject = null;
        float closestDist = Mathf.Infinity;
        foreach(var g in CommandHandler.lightList.Values)
        {
            float dist = Vector3.Distance(g.transform.position, transform.position);
            if (dist < closestDist)
            {
                closestDist = dist;
                closestObject = g;
            }
        }
        return closestObject;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
