using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPAbsorption : MonoBehaviour
{
    private CharacterStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            playerStats.experience += 10;
            Destroy(gameObject);
        }
    }
}
