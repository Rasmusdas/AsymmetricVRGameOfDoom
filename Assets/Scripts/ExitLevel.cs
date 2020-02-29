using System.Collections;
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
                Debug.Log("endgame");
                EndGame();
            }
        }
    }

    public void EndGame()
    {
        LevelManager.LastLevel = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene("EndScene");
    }
}
