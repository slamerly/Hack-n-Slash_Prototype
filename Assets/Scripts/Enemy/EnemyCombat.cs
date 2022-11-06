using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    public float attackDelay = 2f;
    public bool canAttack = false;

    private float attackCooldown = 0;

    private CharacterStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    void Update()
    {
        if (canAttack)
        {
            if(attackCooldown <= 0)
            {
                Attack(playerStats, GetComponent<CharacterStats>().damage);
                attackCooldown = attackDelay;
            }
        }
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats target, float damage)
    {
        target.TakeDamage(damage);
        //Debug.Log(target.name + ": " + target.getLife());
    }
}
