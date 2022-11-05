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
                        Attack(target.GetComponent<CharacterStats>(), GetComponent<CharacterStats>().damage * 3);
                        if(gameObject.tag == "Player" && (playerStats.getLife() + (target.GetComponent<CharacterStats>().getLife() * playerStats.heal) <= playerStats.lifeMax))
                        {
                            Healing(playerStats, target.GetComponent<CharacterStats>().getLife() * playerStats.heal);
                        }
                    }
                    combo = 1;
                    attackCooldown = afterHeavyAttackDelay;
                }
                else
                {
                    //Debug.Log("simple");
                    foreach (GameObject target in detection.enemies)
                    {
                        Attack(target.GetComponent<CharacterStats>(), GetComponent<CharacterStats>().damage);
                        if (gameObject.tag == "Player" && (playerStats.getLife() + (target.GetComponent<CharacterStats>().getLife() * playerStats.heal) <= playerStats.lifeMax))
                        {
                            Healing(playerStats, target.GetComponent<CharacterStats>().getLife() * playerStats.heal);
                        }
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
        Debug.Log(target.name + ": " + target.getLife());
    }

    public void Healing(CharacterStats target, float heal)
    {
        target.TakeHeal(heal);
        Debug.Log(target.name + ": " + target.getLife());
    }
}
