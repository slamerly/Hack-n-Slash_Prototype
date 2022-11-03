using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public SwordDetection detection;
    public float attackDelay = 1f;
    public float afterHeavyAttackDelay = 2f;
    public int combo = 1;
    public int comboLimit = 3;

    private float attackCooldown = 0;

    private Animator animator;
    private CharacterStats playerStats;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        playerStats = GetComponent<CharacterStats>();
    }

    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if (attackCooldown <= 0)
            {
                animator.SetTrigger("Attack");
                if (combo >= comboLimit)
                {
                    //Debug.Log("heavy");
                    foreach(GameObject target in detection.enemies)
                    {
                        Attack(target.GetComponent<CharacterStats>(), playerStats.damage * 3);
                    }
                    combo = 1;
                    attackCooldown = afterHeavyAttackDelay;
                }
                else
                {
                    //Debug.Log("simple");
                    foreach (GameObject target in detection.enemies)
                    {
                        Attack(target.GetComponent<CharacterStats>(), playerStats.damage);
                    }
                    attackCooldown = attackDelay;
                    combo++;
                }
            }
            
        }
        else
        {
            combo = 1;
            //attackCooldown = 1;
        }
        attackCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats target, float damage)
    {
        target.TakeDamage(damage);
        Debug.Log(target.name + ": " + target.life);
    }
}
