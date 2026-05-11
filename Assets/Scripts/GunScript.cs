using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunScript : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform BulletFirePoint;

    [SerializeField] PlayerScript playerScript;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.FireShot)
        {
            GameObject lastShot = Instantiate(bullet);
            lastShot.transform.parent = null;
            lastShot.transform.position = BulletFirePoint.position;
            lastShot = null;
            playerScript.FireShot = false;
        }
    }

}
