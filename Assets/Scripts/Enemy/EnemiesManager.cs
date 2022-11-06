using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    [Header("General")]
    public int nbEnemiesMax = 50;
    public float delaySpawn = 0.1f;

    [Header("Around Player")]
    public int nbMaxEnemeiesIn = 10;
    public float stoppingDistIfMax = 10f;
    public List<GameObject> enemiesIn;
    
    private GameObject[] enemies;
    private GameObject[] spawners;
    private float cooldownSpawn = 0;

    private void Awake()
    {
        spawners = GameObject.FindGameObjectsWithTag("EnemySpawn");
    }

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if(enemies.Length <= nbEnemiesMax && cooldownSpawn <= 0)
        {
            int numSpawn = Random.Range(0, spawners.Length);
            spawners[numSpawn].GetComponent<EnemySpawn>().Spawn();
            cooldownSpawn = delaySpawn;
        }
        cooldownSpawn -= Time.deltaTime;

        if(enemiesIn.Count >= nbMaxEnemeiesIn)
        {
            foreach(GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().agent.stoppingDistance = stoppingDistIfMax;
                for(int i = 0; i < nbMaxEnemeiesIn; i++)
                {
                    if (enemiesIn[i] == enemy)
                    {
                        enemiesIn[i].GetComponent<Enemy>().agent.stoppingDistance = enemy.GetComponent<Enemy>().GetInitStoppingDist();
                    }
                }
            }
        }
        else
        {
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<Enemy>().agent.stoppingDistance = enemy.GetComponent<Enemy>().GetInitStoppingDist();
            }
        }

        for (int i = 0; i < enemiesIn.Count; i++)
        {
            if (enemiesIn[i] == null)
            {
                enemiesIn.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy" && enemiesIn.Count < nbMaxEnemeiesIn)
        {
            enemiesIn.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemiesIn.Remove(other.gameObject);
    }
}
