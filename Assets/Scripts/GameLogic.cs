using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Global Attributes")]
    public float bulletDamage = 1f;

    [Header("Enemy Respawn")]
    List<GameObject> smallEnemies = new List<GameObject>();
    List<GameObject> mediumEnemies = new List<GameObject>();
    GameObject boss;
 
    Enemy enemy;
    private void Start()
    {
        //finds all enemies in the level with the tags,and adds them to seperate lists
        boss = GameObject.FindWithTag("Boss");
        foreach (GameObject smallBoy in GameObject.FindGameObjectsWithTag("SmallEnemy"))
        {
            smallEnemies.Add(smallBoy);
        }
        foreach (GameObject largeBoy in GameObject.FindGameObjectsWithTag("LargeEnemy"))
        {
            mediumEnemies.Add(largeBoy);
        }
    }
    //Respawns all enemies
    public void EnemyRespawn()
    {
       
        //respawns however many enemies are in the list
        for (int i = 0; i < smallEnemies.Count; i++)
        {
            enemy = smallEnemies[i].GetComponent<Enemy>();
            smallEnemies[i].SetActive(true);
            enemy.Respawn();
            enemy.Navigation();
        }
        
        
        
        for (int i = 0; i < mediumEnemies.Count; i++)
        {
            enemy = mediumEnemies[i].GetComponent<Enemy>();
            mediumEnemies[i].SetActive(true);
            enemy.Respawn();
            enemy.Navigation();
        }
        
       
        
        boss.SetActive(true);
        enemy = boss.GetComponent<Enemy>();
        enemy.Respawn();
        enemy.Navigation();
    }
}