using System.Collections;
using UnityEngine;

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
        //Fires the gun when the button for shoot is pressed and removes the parent
        if (playerScript.FireShot)
        {
            GameObject lastShot = Instantiate(bullet);
            lastShot.transform.parent = null;
            lastShot.transform.position = BulletFirePoint.position;
            playerScript.FireShot = false;
            ammoCount -= 1;
        }
    }

    private void Reload()
    {
        //reloads automatically when ammo reaches 0
        if (ammoCount == 0)
        {
            StartCoroutine(ReloadTime());
        }
    }

    private IEnumerator ReloadTime()
    {
        //Reloads the gun and plays an animation and audio file
        gunAnimator.Play("Reload");
        audioSource.Play();
        playerScript.isPlayerReloading = true;
        ammoCount = maxAmmo;
        yield return new WaitForSeconds(1.5f);
        playerScript.isPlayerReloading = false;
    }
}
