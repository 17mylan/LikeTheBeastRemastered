using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuManager : MonoBehaviour
{
    public GameObject Quests;
    public GameObject Credits;
    public GameObject Settings;
    public AudioSource audioSource;
    public AudioClip ClickedButton;
    public PauseMenu pauseMenu;
    public string Url;
    public void ButtonClick(string _String)
    {
        audioSource.PlayOneShot(ClickedButton);
        if(_String == "play")
        {
            SceneManager.LoadScene("jeu");
        }
        if(_String == "credits")
        {
            Quests.SetActive(false);
            Credits.SetActive(true);
            Settings.SetActive(false);
        }
        if (_String == "creditsback")
        {
            Quests.SetActive(true);
            Credits.SetActive(false);
        }
        if (_String == "settings")
        {
            Quests.SetActive(false);
            Settings.SetActive(true);
            Credits.SetActive(false);
        }
        if (_String == "settingsback")
        {
            Quests.SetActive(true);
            Settings.SetActive(false);
        }
        if(_String == "returnmenu")
        {
            SceneManager.LoadScene("Main");
        }
        if (_String == "resume")
        {
            pauseMenu.Resume();
        }
        if (_String == "restart")
        {
            SceneManager.LoadScene("jeu");
        }
    }
    public int allGameFinishedNumber;
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            if (PlayerPrefs.GetInt("allGameFinished") <= 4)
            {
                allGameFinishedNumber = PlayerPrefs.GetInt("allGameFinished") + 1;
                PlayerPrefs.SetInt("allGameFinished", allGameFinishedNumber);
            }
            SceneManager.LoadScene("ScoreScene");
        }
    }
    public void Open()
    {
        Application.OpenURL(Url);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
