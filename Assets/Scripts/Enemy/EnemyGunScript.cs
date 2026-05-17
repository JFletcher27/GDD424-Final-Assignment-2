using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform BulletFirePoint;
    [SerializeField] private float timer = 2f;
    [SerializeField] private float firingTime;
    


    private void Awake()
    {
        
    }

    void Update()
    {
        ShotCooldown();
    }


    private void ShotCooldown()
    {
        firingTime += Time.deltaTime;
        if (firingTime > timer)
        {
            Shoot();
            firingTime = 0f;
        }
    }
    private void Shoot()
    {
        GameObject lastShot = Instantiate(bullet);
        lastShot.transform.position = BulletFirePoint.position;
        lastShot = null;
    }
}
