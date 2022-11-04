using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public TextMeshProUGUI uiLife;
    public TextMeshProUGUI uiExp;
    public TextMeshProUGUI uiLvl;
    public TextMeshProUGUI uiLvlMarket;
    public TextMeshProUGUI uiCombo;
    private CharacterStats playerStats;
    private Combat playerCombat;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
        playerCombat = GameObject.FindGameObjectWithTag("Player").GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        uiLife.text = "Life : " + playerStats.life;
        uiExp.text = "Exp : " + playerStats.experience;
        uiLvl.text = "Lvl : " + playerStats.level;
        uiLvlMarket.text = "Lvl : " + playerStats.level;
        uiCombo.text = "Combo : " + playerCombat.combo;
    }
}
