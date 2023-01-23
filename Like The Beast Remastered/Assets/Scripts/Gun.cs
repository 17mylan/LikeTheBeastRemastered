using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f;
    public Texture2D curseur;

    public Camera fpscam;
    public ParticleSystem Flash;
    public GameObject impactEffect;

    public GameObject gun;
    public float throwForce = 40f;
    public float plasmabullet = 4f;
    public GameObject grenadePrefab;
    public GameObject PlasmaBall;
    public GameObject cible;
    public Animator mAnimator;

    private void Start()
    {
        PlayerPrefs.SetFloat("LastGameScore", 0);
        PlayerPrefs.SetInt("lastGameRobotsKilled", 0);
        PlayerPrefs.SetInt("lastGameSpidersKilled", 0);
    }

    void Update()
    {
        ///Ray ray = fpscam.ScreenPointToRay(Input.mousePosition);
        ///if (Physics.Raycast(ray, out RaycastHit raycastHit))
        ///{
        ///    transform.position = raycastHit.point;
        ///}
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = fpscam.ScreenToWorldPoint(mousePos);
        Debug.DrawRay(transform.position, mousePos - transform.position,
        Color.blue);
        mAnimator = gun.GetComponent<Animator>();
        mAnimator.SetBool("tir", false);
        if (GameObject.Find("PauseMenu") == false)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                if (GameObject.Find("gun") == true)
                {
                    Shoot();
                }
                else if (GameObject.Find("gun2") == true)
                {
                    Plasma();
                }
            }

            if (Input.GetKeyDown("g"))
            {
                ThrowGrenade();
            }
        }
    }
    public GameObject muzzleFlash;
    public GameObject muzzleFlash2;
    public AudioSource audioSourceShot;
    public AudioClip gunFire;
    public AudioClip targetHit;
    public GameObject addPointsUI;

    public int robotsKilled;
    public int spidersKilled;
    public int allRobotsKilled;
    public int allSpidersKilled;
    void Shoot()
    {
        mAnimator = gun.GetComponent<Animator>();
        mAnimator.SetBool("tir", true);
        muzzleFlash.SetActive(true);
        audioSourceShot.PlayOneShot(gunFire);
        StartCoroutine("muzzleFlashOff");
        RaycastHit hit;
        Ray ray = fpscam.ScreenPointToRay(Input.mousePosition);
        //Debug.DrawRay(transform.position, transform.forward, Color.red);
        if (Physics.Raycast(ray, out hit, 300))
        {
            Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));



            Kill target = hit.transform.GetComponent<Kill>();
            if (target != null)
            {
                target.Die();
                if (target.tag == "robots")
                {
                    robotsKilled = PlayerPrefs.GetInt("lastGameRobotsKilled");
                    robotsKilled++;
                    PlayerPrefs.SetInt("lastGameRobotsKilled", robotsKilled);
                    if(PlayerPrefs.GetInt("allRobotsKilled") <= 299)
                    {
                        allRobotsKilled = PlayerPrefs.GetInt("allRobotsKilled") + 1;
                        PlayerPrefs.SetInt("allRobotsKilled", allRobotsKilled);
                    }
                }
                else if (target.tag == "spiders")
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
                StartCoroutine("removeAddPointsUI");
                StartCoroutine("removeImpactEffect");
            }
        }
    }
    IEnumerator removeAddPointsUI()
    {
        addPointsUI.SetActive(true);
        audioSourceShot.PlayOneShot(targetHit);
        yield return new WaitForSeconds(0.4f);
        addPointsUI.SetActive(false);
    }
    IEnumerator removeImpactEffect()
    {
        yield return new WaitForSeconds(2);
        DestroyImmediate(GameObject.Find("impactEffect(Clone)"), true);
    }
    IEnumerator muzzleFlashOff()
    {
        yield return new WaitForSeconds(.05f);
        muzzleFlash.SetActive(false);
        muzzleFlash2.SetActive(false);
    }
    void Plasma()
    {
        muzzleFlash2.SetActive(true);
        audioSourceShot.PlayOneShot(gunFire);
        StartCoroutine("muzzleFlashOff");
        mAnimator = gun.GetComponent<Animator>();
        mAnimator.SetBool("tirr", true);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = fpscam.ScreenToWorldPoint(mousePos);
        GameObject PlasmaBal = Instantiate(PlasmaBall, transform.position, transform.rotation);
        Rigidbody rb = PlasmaBal.GetComponent<Rigidbody>();
        rb.AddForce((mousePos - transform.position) / 5, ForceMode.VelocityChange);
    }
    void ThrowGrenade()
    {
        ///RaycastHit hit;
        ///Ray ray = fpscam.ScreenPointToRay(Input.mousePosition);
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 100f;
        mousePos = fpscam.ScreenToWorldPoint(mousePos);
        GameObject grenade = Instantiate(grenadePrefab, transform.position, transform.rotation);
        Rigidbody rb = grenade.GetComponent<Rigidbody>();
        rb.AddForce((mousePos - transform.position) / 3, ForceMode.VelocityChange);
    }
}
