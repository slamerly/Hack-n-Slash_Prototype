using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float life;
    public float damage;
    
    public int level = 0;
    public int expToNextLevel = 100;
    [Header("Pour les ennemis, fait apparaitre 1 sphere tout les 10")]
    public int experience = 0;

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
}
