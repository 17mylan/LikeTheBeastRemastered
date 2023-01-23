using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plasma : MonoBehaviour
{
    public void OnCollisionEnter(Collision collision)
    {
        if(gameObject.name == "Sphere(Clone)")
        {
            if(collision.gameObject.tag == "robots" || collision.gameObject.tag == "spiders")
            {
                Destroy(collision.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
