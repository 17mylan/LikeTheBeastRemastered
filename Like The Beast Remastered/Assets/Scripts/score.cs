using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class score : MonoBehaviour
{
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI lastGameScore;
    public TextMeshProUGUI robotsCounter;
    public TextMeshProUGUI spidersCounter;

    public TextMeshProUGUI shotAccuracy;
    public float pourcentage;
    public float points;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "ScoreScene")
        {
            pourcentage = PlayerPrefs.GetFloat("lastGameShootTouched") / PlayerPrefs.GetFloat("lastGameShootFired") * 100;
            
            if(float.IsNaN(pourcentage))
            {
                shotAccuracy.text = "0".ToString();
            }
            else
            {
                shotAccuracy.text = pourcentage.ToString("0.0");
            }

            lastGameScore.text = PlayerPrefs.GetFloat("LastGameScore", points).ToString();
            robotsCounter.text = PlayerPrefs.GetInt("lastGameRobotsKilled").ToString();
            spidersCounter.text = PlayerPrefs.GetInt("lastGameSpidersKilled").ToString();
            
            PlayerPrefs.SetFloat("LastGameScore", 0);
            PlayerPrefs.SetInt("lastGameRobotsKilled", 0);
            PlayerPrefs.SetInt("lastGameSpidersKilled", 0);
            PlayerPrefs.SetFloat("lastGameShootFired", 0);
            PlayerPrefs.SetFloat("lastGameShootTouched", 0);
        }
    }
    void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "jeu")
        {
            scoreUI.text = points.ToString();
        }
    }
}
