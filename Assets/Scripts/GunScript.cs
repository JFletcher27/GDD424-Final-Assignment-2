using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform BulletFirePoint;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            OnShoot();
        }
    }

    private void OnShoot()
    {
        GameObject lastShot = Instantiate(bullet);
        lastShot.transform.parent = null;
        lastShot.transform.position = BulletFirePoint.position;
        lastShot = null;
    }
}
