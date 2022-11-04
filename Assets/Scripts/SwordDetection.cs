using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    public List<GameObject> enemies;

    private void Update()
    {
        for(int i = 0; i < enemies.Count; i++)
        {
            if (enemies[i] == null)
            {
                enemies.RemoveAt(i);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            enemies.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        enemies.Remove(other.gameObject);
    }
}
