using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DuffelbagManager : MonoBehaviour
{
    public static bool HasTakenGold { get; set; }
    public static float take;
    public float maxWorth;
    public float minWorth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "GoldBar")
        {
            HasTakenGold = true;
            take += Random.Range(minWorth, maxWorth);
            Destroy(other.gameObject);
        }
    }
}
