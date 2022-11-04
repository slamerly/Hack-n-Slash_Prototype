using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public TextMeshProUGUI UILife;
    public TextMeshProUGUI UIExp;
    public TextMeshProUGUI UILvl;
    private CharacterStats playerStats;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        UILife.text = "Life : " + playerStats.life;
        UIExp.text = "Exp : " + playerStats.experience;
        UILvl.text = "Lvl : " + playerStats.level;
    }
}
