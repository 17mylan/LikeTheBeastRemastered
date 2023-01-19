using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grenade : MonoBehaviour
{
    public float delay = 1f;
    float countdown;
    bool hasExploded = false;
    public GameObject explosionEffect;
    public float radius = 8f;
    public float force = 100f;
    public CameraShake cameraShake;
    // Start is called before the first frame update
    void Start()
    {
        countdown = delay;
    }

    // Update is called once per frame
    void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f && !hasExploded)
        {
            Explode();
            hasExploded = true;
        }
    }

    void Explode()
    {
        Instantiate(explosionEffect, transform.position, transform.rotation);
        Collider[] colliders = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in colliders)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }

            Kill dest = nearbyObject.GetComponent<Kill>();
            if (dest != null)
            {
                dest.casser();
            }
        }
        StartCoroutine(cameraShake.Shake(2f, 2f));
        Destroy(gameObject);
    }
}
