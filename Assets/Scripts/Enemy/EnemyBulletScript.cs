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
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }

}
