using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesManager : MonoBehaviour
{
    public int nbMaxEnemeiesIn = 10;
    public float stoppingDistIfMax = 10f;
    public List<GameObject> enemiesIn;
    
    private GameObject[] enemies;

    private void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

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
