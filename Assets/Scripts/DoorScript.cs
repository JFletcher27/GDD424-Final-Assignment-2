using UnityEngine;

public class DoorScript : MonoBehaviour
{

    [SerializeField] Animator doorAnimator;
    [SerializeField] GameObject player;
    [SerializeField] Transform playerTransform;
    [SerializeField] float openingDistance = 3f;
    // Start is called before the first frame update
    void Start()
    {
        doorAnimator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player");
        playerTransform = player.transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //when the player is close enough to the door, play the open door animation
        float dist = Vector3.Distance(playerTransform.position, transform.position);

        if (doorAnimator != null)
        {
            if (dist <= openingDistance)
            {
                doorAnimator.SetTrigger("PlayerOpenDoor");
            }
        }
    }


}
