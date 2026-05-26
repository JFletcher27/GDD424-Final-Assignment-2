using System.Collections;
using UnityEngine;

public class EnemyBulletScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Rigidbody rb;
    [SerializeField] public Transform rifle;

    [Header("Bullet Attributes")]
    [SerializeField] float bulletSpeed = 10f;

    private void Awake()
    {
        //finds the bullets rigidbody, and propels it forward in the direction the gun is facing
        rb = GetComponent<Rigidbody>();
        Vector3 dir = transform.parent.forward;
        rb.AddForce(dir * bulletSpeed, ForceMode.Impulse);
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(DeleteTime());
    }

    private IEnumerator DeleteTime()
    {
        //Destroys itself after 2 seconds
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
