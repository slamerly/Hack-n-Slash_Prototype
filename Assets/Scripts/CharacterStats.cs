using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public float life;
    public float damage;

    public void TakeDamage(float damage)
    {
        life -= damage;
    }
}
