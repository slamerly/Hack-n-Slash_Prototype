using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public void Spawn()
    {
        float xMin, xMax, zMin, zMax, xPos, zPos;

        xMin = transform.position.x - (transform.localScale.x / 2);
        xMax = transform.position.x + (transform.localScale.x / 2);
        zMin = transform.position.z - (transform.localScale.z / 2);
        zMax = transform.position.z + (transform.localScale.z / 2);

        xPos = Random.Range(xMin, xMax);
        zPos = Random.Range(zMin, zMax);

        Instantiate(enemy, new Vector3(xPos, transform.position.y + 0.25f, zPos), Quaternion.identity);
    }
}
