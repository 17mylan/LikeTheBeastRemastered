using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public GameObject grenadePrefab;
    public GameObject cible;
    public Animator mAnimator;

    public GameObject muzzleFlash;
    public AudioSource audioSourceShot;
    public AudioClip gunFire;
    public AudioClip targetHit;
    public GameObject addPointsUI;

    public int robotsKilled;
    public int spidersKilled;
    public int allRobotsKilled;
    public int allSpidersKilled;
    public CameraShake cameraShake;

    public int ammoCount = 30;
    public TextMeshProUGUI ammoCounter;
    public GameObject reloadAnnonce;
    public GameObject reloadPressAnnonce;

    public bool isReloading = false;
    public AudioSource weaponReloadSource;
    public AudioClip weaponReloadingSong;
    public AudioSource weapon;
    public AudioClip weaponLowAmmo;

    public GameObject uiAmmo1;
    public GameObject uiAmmo2;
    public GameObject uiAmmo3;

    public int grenadeNumber = 10;
    public TextMeshProUGUI grenadeCount;

    private void Start()
    {
        PlayerPrefs.SetFloat("LastGameScore", 0);
        PlayerPrefs.SetInt("lastGameRobotsKilled", 0);
        PlayerPrefs.SetInt("lastGameSpidersKilled", 0);
    }

    void Update()
    {
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
                    if (isReloading == false)
                    {
                        if(ammoCount >= 1)
                        {
                            Shoot();
                            ammoCount = ammoCount - 1;
                            ammoCounter.text = ammoCount.ToString();
                            if(ammoCount <= 20)
                            {
                                uiAmmo1.SetActive(false);
                            }
                            if(ammoCount < 11)
                            {
                                if(isReloading == false)
                                {
                                    reloadPressAnnonce.SetActive(true);
                                    uiAmmo2.SetActive(false);
                                }
                            }
                            if (ammoCount <= 0)
                            {
                                reloadPressAnnonce.SetActive(false);
                                uiAmmo3.SetActive(false);
                                reloadAnnonce.SetActive(false);
                                StartCoroutine("weaponReloading");
                                isReloading = true;
                            }
                        }
                    }
                }
            }

            if (Input.GetKeyDown("g"))
            {
                if(grenadeNumber >= 1)
                {
                    ThrowGrenade();
                    grenadeNumber = grenadeNumber - 1;
                    grenadeCount.text = grenadeNumber.ToString();
                }
            }
            if(Input.GetKeyDown("r"))
            {
                if (isReloading == false)
                {
                    if(ammoCount < 30)
                    {
                        isReloading = true;
                        reloadPressAnnonce.SetActive(false);
                        reloadAnnonce.SetActive(false);
                        StartCoroutine("weaponReloading");
                    }
                }
            }
        }
    }
    IEnumerator weaponReloading()
    {
        reloadAnnonce.SetActive(true);
        weaponReloadSource.PlayOneShot(weaponReloadingSong);
        yield return new WaitForSeconds(2.3f);
        uiAmmo1.SetActive(true);
        uiAmmo2.SetActive(true);
        uiAmmo3.SetActive(true);
        ammoCount = 30;
        reloadAnnonce.SetActive(false);
        ammoCounter.text = ammoCount.ToString();
        isReloading = false;
    }
    void Shoot()
    {
        if(ammoCount > 10)
        {
            audioSourceShot.PlayOneShot(gunFire);
        }
        else if(ammoCount <= 10)
        {
            weapon.PlayOneShot(weaponLowAmmo);
        }
        cameraShake.Invoke("shake", 0.3f);
        mAnimator = gun.GetComponent<Animator>();
        mAnimator.SetBool("tir", true);
        muzzleFlash.SetActive(true);
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
