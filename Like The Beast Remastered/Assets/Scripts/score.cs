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
    public float points;
    void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "ScoreScene")
        {
            lastGameScore.text = PlayerPrefs.GetFloat("LastGameScore", points).ToString();
            robotsCounter.text = PlayerPrefs.GetInt("lastGameRobotsKilled").ToString();
            spidersCounter.text = PlayerPrefs.GetInt("lastGameSpidersKilled").ToString();
            
            PlayerPrefs.SetFloat("LastGameScore", 0);
            PlayerPrefs.SetInt("lastGameRobotsKilled", 0);
            PlayerPrefs.SetInt("lastGameSpidersKilled", 0);
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
