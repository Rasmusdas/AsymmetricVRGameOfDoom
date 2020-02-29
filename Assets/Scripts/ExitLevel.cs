﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (DuffelbagManager.HasTakenGold)
            {
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        SceneManager.LoadScene("EndScene");
    }
}
