using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadQuests : MonoBehaviour
{
    public TextMeshProUGUI allRobotsKilled;
    public TextMeshProUGUI allSpidersKilled;
    public TextMeshProUGUI allGameFinished;
    public TextMeshProUGUI allTimeScore;
    public void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Main")
        {
            allRobotsKilled.text = PlayerPrefs.GetInt("allRobotsKilled").ToString();
            allSpidersKilled.text = PlayerPrefs.GetInt("allSpidersKilled").ToString();
            allGameFinished.text = PlayerPrefs.GetInt("allGameFinished").ToString();
            allTimeScore.text = PlayerPrefs.GetFloat("allTimeScore").ToString();
        }
    }
    private void Update()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        string sceneName = currentScene.name;
        if (sceneName == "Main")
        {

            if (Input.GetKeyDown(KeyCode.Space))
            {
                PlayerPrefs.SetInt("allRobotsKilled", 299);
                PlayerPrefs.SetInt("allSpidersKilled", 299);
                PlayerPrefs.SetInt("allGameFinished", 4);
                PlayerPrefs.SetFloat("allTimeScore", 49900);
            }
            allRobotsKilled.text = PlayerPrefs.GetInt("allRobotsKilled").ToString();
            allSpidersKilled.text = PlayerPrefs.GetInt("allSpidersKilled").ToString();
            allGameFinished.text = PlayerPrefs.GetInt("allGameFinished").ToString();
            allTimeScore.text = PlayerPrefs.GetFloat("allTimeScore").ToString();
        }
    }
}
