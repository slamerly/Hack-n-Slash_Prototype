using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject exp;
    public int xpDispertion = 3;

    private CharacterStats stats;

    private void Awake()
    {
        stats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        if(stats.getLife() <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        int xpos = 0;
        int zpos = 0;
        for(int i = 0; i < stats.experience/10 ; i++)
        {
            xpos = Random.Range(-xpDispertion, xpDispertion);
            zpos = Random.Range(-xpDispertion, xpDispertion);
            Instantiate(exp, new Vector3(xpos + transform.position.x, 0.75f, zpos + transform.position.z), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
