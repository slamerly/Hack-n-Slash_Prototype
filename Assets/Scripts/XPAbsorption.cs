using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XPAbsorption : MonoBehaviour
{
    public float speed = 0.03f;
    private GameObject player;
    private CharacterStats playerStats;


    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<CharacterStats>();
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
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
