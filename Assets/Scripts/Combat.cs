using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public SwordDetection forwardDetection;
    public SwordDetection aoeDetection;
    public float heavyMultyDamage = 3f;
    public float aoeMultyDamage = 3f;
    public float attackDelay = 1f;
    public float afterHeavyAttackDelay = 2f;
    public float aoeDelay = 3f;
    public float aoeRadius = 5f;
    public int combo = 1;
    public int comboLimit = 3;

    bool aoeActive = false;
    private float attackCooldown = 0;
    private float aoeCooldown = 0;

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
                    // HEAVY ATTACK
                    foreach(GameObject target in forwardDetection.enemies)
                    {
                        if(gameObject.tag == "Player" && (playerStats.getLife() + (target.GetComponent<CharacterStats>().getLife() * playerStats.heal) <= playerStats.lifeMax))
                        {
                            Healing(playerStats, target.GetComponent<CharacterStats>().getLife() * playerStats.heal);
                        }
                        Attack(target.GetComponent<CharacterStats>(), GetComponent<CharacterStats>().damage * heavyMultyDamage);
                    }
                    combo = 1;
                    attackCooldown = afterHeavyAttackDelay;
                }
                else
                {
                    // SIMPLE ATTACK
                    foreach (GameObject target in forwardDetection.enemies)
                    {
                        if (gameObject.tag == "Player" && (playerStats.getLife() + (target.GetComponent<CharacterStats>().getLife() * playerStats.heal) <= playerStats.lifeMax))
                        {
                            Healing(playerStats, target.GetComponent<CharacterStats>().getLife() * playerStats.heal);
                        }
                        Attack(target.GetComponent<CharacterStats>(), GetComponent<CharacterStats>().damage);
                    }
                    attackCooldown = attackDelay;
                    combo++;
                }
            }
        }
        else
        {
            combo = 1;
        }
        attackCooldown -= Time.deltaTime;


        // AOE
        aoeDetection.transform.localScale = new Vector3(aoeRadius, 0, 0);
        if (aoeActive && Input.GetButton("Fire2"))
        {
            if (aoeCooldown <= 0)
            {
                animator.SetTrigger("AoE");
                foreach (GameObject target in aoeDetection.enemies)
                {
                    if (gameObject.tag == "Player" && (playerStats.getLife() + (target.GetComponent<CharacterStats>().getLife() * playerStats.heal) <= playerStats.lifeMax))
                    {
                        Healing(playerStats, target.GetComponent<CharacterStats>().getLife() * playerStats.heal);
                    }
                    Attack(target.GetComponent<CharacterStats>(), GetComponent<CharacterStats>().damage * aoeMultyDamage);
                }
                aoeCooldown = aoeDelay;
            }
        }
        aoeCooldown -= Time.deltaTime;
    }

    public void Attack(CharacterStats target, float damage)
    {
        target.TakeDamage(damage);
        //Debug.Log(target.name + ": " + target.getLife());
    }

    public void Healing(CharacterStats target, float heal)
    {
        target.TakeHeal(heal);
        //Debug.Log(target.name + ": " + target.getLife());
    }

    public void AoeActivation(bool activation)
    {
        aoeActive = activation;
    }

    public bool GetAoeActivation()
    {
        return aoeActive;
    }
}
