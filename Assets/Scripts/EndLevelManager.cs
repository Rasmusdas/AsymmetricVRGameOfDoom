using System.Collections;
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

    void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
