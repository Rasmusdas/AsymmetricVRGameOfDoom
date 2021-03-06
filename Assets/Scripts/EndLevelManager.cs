﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndLevelManager : MonoBehaviour
{
    public List<Text> takeTexts;

    // Start is called before the first frame update
    void Start()
    {
        foreach (Text t in takeTexts)
        {
            t.text = "Take: " + DuffelbagManager.take;
        }
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        if (LevelManager.LastLevel == "Tutorial")
        {
            SceneManager.LoadScene("Level 1");
        }
        else
        {
            int.TryParse(LevelManager.LastLevel.Split(' ')[1], out int number);
            DuffelbagManager.HasTakenGold = false;
            SceneManager.LoadScene("Level " + (number+1));
        }
    }
}
