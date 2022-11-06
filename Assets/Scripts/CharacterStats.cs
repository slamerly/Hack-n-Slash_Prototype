using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public UI_HealthBar healthBar = null;
    public float lifeMax;
    public float damage;
    public float heal = 0.1f;
    public float invincibility = 0.1f;
    
    public int level = 0;
    public int expToNextLevel = 100;
    [Header("Pour les ennemis, fait apparaitre 1 sphere tout les 10")]
    public int experience = 0;

    public List<string> skills;

    float life;
    float timerInvi = 0;
    Vector3 initPositionPlayer;

    private void Awake()
    {
        life = lifeMax;
        initPositionPlayer = transform.position;
        if(gameObject.tag == "Enemy")
            healthBar.SetMaxHealth(lifeMax);
    }

    private void Update()
    {
        if (experience > expToNextLevel - 1 && gameObject.tag == "Player")
        {
            level++;
            experience = 0;
        }
        if (gameObject.tag == "Enemy")
            healthBar.SetHealth(life);

        if(gameObject.tag == "Player" && life <= 0)
        {
            gameObject.transform.position = initPositionPlayer;
            life = lifeMax;
        }

        timerInvi -= Time.deltaTime;
    }

    public void TakeDamage(float dam)
    {
        if (gameObject.tag == "Player")
        {
            if (timerInvi <= 0)
            {
                life -= dam;
                timerInvi = invincibility;
            }
        }
        else
            life -= dam;
    }

    public void TakeHeal(float heal)
    {
        life += heal;
    }

    public float getLife()
    {
        return life;
    }
}
