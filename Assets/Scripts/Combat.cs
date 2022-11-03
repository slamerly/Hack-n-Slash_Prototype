using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public Animator animator;
    public CharacterStats playerStats;
    public CharacterStats targetStats;
    public float attackDelay = 1f;
    public int combo = 1;
    public int comboLimit = 3;

    private float attackCooldown = 0;
    void Update()
    {
        if(Input.GetButton("Fire1"))
        {
            if (attackCooldown <= 0)
            {
                animator.SetTrigger("Attack");
                if (combo == comboLimit)
                {
                    Debug.Log("heavy");
                    Attack(playerStats.damage * 3);
                    combo = 1;
                    attackCooldown = attackDelay * 3;
                }
                else
                {
                    Debug.Log("simple");
                    Attack(playerStats.damage);
                    attackCooldown = attackDelay;
                    combo++;
                }
            }
            attackCooldown -= Time.deltaTime;
        }
        else
        {
            combo = 1;
            attackCooldown = 0;
        }
    }

    public void Attack(float damage)
    {
        targetStats.TakeDamage(damage);
        Debug.Log(targetStats.life);
    }
}
