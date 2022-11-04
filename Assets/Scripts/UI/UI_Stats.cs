using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Stats : MonoBehaviour
{
    public TextMeshProUGUI UILife;
    public TextMeshProUGUI UIExp;
    public TextMeshProUGUI UILvl;
    public TextMeshProUGUI UICombo;
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
        UILife.text = "Life : " + playerStats.life;
        UIExp.text = "Exp : " + playerStats.experience;
        UILvl.text = "Lvl : " + playerStats.level;
        UICombo.text = "Combo : " + playerCombat.combo;
    }
}
