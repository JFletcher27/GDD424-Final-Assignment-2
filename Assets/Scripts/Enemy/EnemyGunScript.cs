using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform BulletFirePoint;
    [SerializeField] Enemy enemyScript;
    [SerializeField] private float firingTime;
    
    


    private void Awake()
    {
        enemyScript = GetComponentInParent<Enemy>();

    }

    void Update()
    {
        ShotCooldown();
    }


    private void ShotCooldown()
    {
        firingTime += Time.deltaTime;
        if (firingTime > enemyScript.shotSpeed)
        {
            Shoot();
            firingTime = 0f;
        }
    }
    private void Shoot()
    {
        GameObject lastShot = Instantiate(bullet, BulletFirePoint);
        lastShot.transform.position = BulletFirePoint.position;
    }
}
