using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] Rigidbody rb;

    [Header("Bullet Attributes")]
    [SerializeField] float bulletSpeed = 10f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 dir = Camera.main.transform.forward; 
        rb.AddForce(dir * bulletSpeed,ForceMode.Impulse);
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
