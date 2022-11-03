using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordDetection : MonoBehaviour
{
    public List<GameObject> enemies;

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
