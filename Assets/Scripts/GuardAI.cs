using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        foreach(GameObject g in CommandHandler.lightList.Values)
        {
            if (!Physics.Raycast(transform.position, g.transform.position - transform.position, out RaycastHit lightHit, spottingRange))
            {
                lightsOff = false;
                break;
            }
            else
            {
                lightsOff = true;
            }
        }
        if(!lightsOff)
        {
            if (Physics.Raycast(transform.position, player.position - transform.position, out RaycastHit hit, spottingRange))
            {
                Debug.Log(hit.transform.tag);
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
                Debug.Log(Vector3.Distance(lastTargetPosition, transform.position));
                currentTarget = lastTargetPosition;
            }
            else
            {
                lastTargetPosition = startPosition;
                currentTarget = startPosition;
            }
            agent.destination = currentTarget;
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
}
