using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public GameObject PatchNote;
    public GameObject Credits;
    public GameObject Settings;
    public AudioSource audioSource;
    public AudioClip ClickedButton;
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
            PatchNote.SetActive(false);
            Credits.SetActive(true);
            Settings.SetActive(false);
        }
        if (_String == "creditsback")
        {
            PatchNote.SetActive(true);
            Credits.SetActive(false);
        }
        if (_String == "settings")
        {
            PatchNote.SetActive(false);
            Settings.SetActive(true);
            Credits.SetActive(false);
        }
        if (_String == "settingsback")
        {
            PatchNote.SetActive(true);
            Settings.SetActive(false);
            //audiosourceplay
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Finish")
        {
            SceneManager.LoadScene("Main");
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
