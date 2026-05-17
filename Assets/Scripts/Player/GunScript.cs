using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform BulletFirePoint;
    [SerializeField] PlayerScript playerScript;
    [SerializeField] Animator gunAnimator;
    [SerializeField] AudioSource audioSource;

    [Header("Gun Attributes")]
    [SerializeField] float maxAmmo = 10;
    [SerializeField] float ammoCount;

    private void Awake()
    {
        ammoCount = maxAmmo;
        if (gunAnimator == null)
        {
            gunAnimator = GetComponent<Animator>();
        }

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        Shoot();
        Reload();
    }

    private void Shoot()
    {
        if (playerScript.FireShot)
        {
            GameObject lastShot = Instantiate(bullet);
            lastShot.transform.parent = null;
            lastShot.transform.position = BulletFirePoint.position;
            lastShot = null;
            playerScript.FireShot = false;
            ammoCount -= 1;
        }
    }

    private void Reload()
    {
        if (ammoCount == 0)
        {
            StartCoroutine(ReloadTime());
        }
    }

    private IEnumerator ReloadTime()
    {
        gunAnimator.Play("Reload");
        audioSource.Play();
        playerScript.isPlayerReloading = true;
        ammoCount = maxAmmo;
        yield return new WaitForSeconds(1.5f);
        playerScript.isPlayerReloading = false;
    }
}
