using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public float delay = 2f;
    public bool timer;
    public score score;

    private void Update()
    {
        if (timer) 
            {
                delay -= Time.deltaTime;
                poof();
            }
    }

    internal void casser()
    {
        timer = true;
    }
    public void poof()
    {
        if (delay <= 0f)
        {
            score.points = score.points + 50;
            if (PlayerPrefs.GetFloat("allTimeScore") <= 49950)
            {
                allTimeScore = PlayerPrefs.GetFloat("allTimeScore") + 50;
                PlayerPrefs.SetFloat("allTimeScore", allTimeScore);
            }
            Destroy(gameObject);
        }
    }
    public int robotsKilled = 0;
    public int spidersKilled = 0;
    public int allRobotsKilled;
    public int allSpidersKilled;
    public float allTimeScore;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere(Clone)")
        {
            score.points += 50;
            PlayerPrefs.SetFloat("LastGameScore", score.points);
            if (PlayerPrefs.GetFloat("allTimeScore") <= 49950)
            {
                allTimeScore = PlayerPrefs.GetFloat("allTimeScore") + 50;
                PlayerPrefs.SetFloat("allTimeScore", allTimeScore);
            }
            if (gameObject.tag == "robots")
            {
                robotsKilled = PlayerPrefs.GetInt("lastGameRobotsKilled");
                robotsKilled++;
                PlayerPrefs.SetInt("lastGameRobotsKilled", robotsKilled);
                if (PlayerPrefs.GetInt("allRobotsKilled") <= 299)
                {
                    allRobotsKilled = PlayerPrefs.GetInt("allRobotsKilled") + 1;
                    PlayerPrefs.SetInt("allRobotsKilled", allRobotsKilled);
                }
            }
            else if(gameObject.tag == "spiders")
            {
                spidersKilled = PlayerPrefs.GetInt("lastGameSpidersKilled");
                spidersKilled++;
                PlayerPrefs.SetInt("lastGameSpidersKilled", spidersKilled);
                if (PlayerPrefs.GetInt("allSpidersKilled") <= 299)
                {
                    allSpidersKilled = PlayerPrefs.GetInt("allSpidersKilled") + 1;
                    PlayerPrefs.SetInt("allSpidersKilled", allSpidersKilled);
                }
            }
            //Destroy(collision.gameObject);
            //Destroy(gameObject);
        }
    }
    public void Die()
    {
        score.points = score.points + 50;
        PlayerPrefs.SetFloat("LastGameScore", score.points);
        if(PlayerPrefs.GetFloat("allTimeScore") <= 49950)
        {
            allTimeScore = PlayerPrefs.GetFloat("allTimeScore") + 50;
            PlayerPrefs.SetFloat("allTimeScore", allTimeScore);
        }
        Destroy(gameObject);
        DestroyImmediate(GameObject.Find("Sphere(Clone)"), true);
    }
}
