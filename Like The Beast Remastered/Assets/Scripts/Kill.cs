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
            Destroy(gameObject);   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Sphere(Clone)")
        {
            Destroy(gameObject);
            score.points += 50;
        }
    }
    public void Die()
    {
        score.points = score.points + 50;
        PlayerPrefs.SetFloat("LastGameScore", score.points);
        Destroy(gameObject);
        DestroyImmediate(GameObject.Find("Sphere(Clone)"), true);
    }
}
