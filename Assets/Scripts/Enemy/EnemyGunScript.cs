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
        //enemies have a set shot speed, gun counts up and when it reaches the shot speed value, it fires and resets the firing timer
        firingTime += Time.deltaTime;
        if (firingTime > enemyScript.shotSpeed)
        {
            Shoot();
            firingTime = 0f;
        }
    }

    private void Shoot()
    {
        //instantiates the bullet
        GameObject lastShot = Instantiate(bullet, bulletFirePoint);
        lastShot.transform.position = bulletFirePoint.position;
    }
}
