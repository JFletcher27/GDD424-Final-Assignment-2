using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("References")]
    [SerializeField] PlayerScript playerScript;
    [SerializeField] GameLogic gameLogic;
    [SerializeField] NavMeshAgent agent;

    [Header("Enemy Attributes")]
    [SerializeField] public float currentHp;
    [SerializeField] private float maxHp;
    [SerializeField] public float shotSpeed;

    [Header("AI Settings")]
    [SerializeField] float speed;
    [SerializeField] string arena;
    private readonly List<Transform> pathfinding = new();
    private int path;


    private void Awake()
    {
        //Gets the enemies stats
        if (gameObject.CompareTag("SmallEnemy"))
        {
            maxHp = 10f;
            shotSpeed = 2f;
            speed = 5f;
        }
        else if (gameObject.CompareTag("LargeEnemy"))
        {
            maxHp = 15f;
            shotSpeed = 1f;
            speed = 4f;
        }
        else if (gameObject.CompareTag("Boss"))
        {
            maxHp = 30f;
            shotSpeed = 0.5f;
            speed = 3f;
        }

        agent = GetComponent<NavMeshAgent>();

        agent.speed = speed;

        currentHp = maxHp;
        //adds each tile of the arenas to the navmesh
        foreach (GameObject obj in GameObject.FindGameObjectsWithTag(arena))
        {
            pathfinding.Add(obj.transform);
        }

        Navigation();
    }

    void Update()
    {
        //if enemy is too close to the pathfinding point, find a new one
        if (Vector3.Distance(transform.position, pathfinding[path].position) < 2f)
        {
            Navigation();
        }

        Death();
    }

    public void Navigation()
    {
        //find random spot in the arena the enemy is in and move towards it
        path = Random.Range(0, pathfinding.Count);
        agent.SetDestination(pathfinding[path].position);
    }

    private void Death()
    {
        if (currentHp <= 0f)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            currentHp -= gameLogic.bulletDamage;
        }

    }

    public void Respawn()
    {
        currentHp = maxHp;
    }
}
