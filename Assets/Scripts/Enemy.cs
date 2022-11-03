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
        if(stats.life <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("wft");
        int xpos = 0;
        int zpos = 0;
        for(int i = 0; i < stats.experience/10 ; i++)
        {
            Debug.Log("hello");
            xpos = Random.Range(0, xpDispertion);
            zpos = Random.Range(0, xpDispertion);
            Instantiate(exp, new Vector3(xpos, 0.75f, zpos), Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
