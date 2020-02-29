using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuffelbagManager : MonoBehaviour
{
    public bool HasTakenGold { get { return take > 0; } }
    public float take;
    public float maxWorth;
    public float minWorth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoldBar")
        {
            take += Random.Range(minWorth, maxWorth);
            Destroy(other.gameObject);
        }
    }
}
