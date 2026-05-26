using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        //destroy bullet on collision with wall
        if (collider.gameObject.CompareTag("Bullet"))
        {
            Destroy(collider.gameObject);
        }
    }
}
