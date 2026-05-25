using UnityEngine;

public class EnemyGunScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] GameObject bullet;
    [SerializeField] Transform bulletFirePoint;
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
        GameObject lastShot = Instantiate(bullet, bulletFirePoint);
        lastShot.transform.position = bulletFirePoint.position;
    }
}
