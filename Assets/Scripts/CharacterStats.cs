using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{

    public float lifeMax;
    public float damage;
    public float heal = 0.1f;
    
    public int level = 0;
    public int expToNextLevel = 100;
    [Header("Pour les ennemis, fait apparaitre 1 sphere tout les 10")]
    public int experience = 0;

    public List<string> skills;

    float life;

    private void Awake()
    {
        life = lifeMax;
    }

    private void Update()
    {
        if (experience > expToNextLevel)
        {
            level++;
            experience = 0;
        }
    }

    public void TakeDamage(float dam)
    {
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
