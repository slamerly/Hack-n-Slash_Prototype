using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Market : MonoBehaviour
{
    public Market market;
    public TextMeshProUGUI playerLVL;
    public TextMeshProUGUI textButtonV;
    public TextMeshProUGUI costV;
    public TextMeshProUGUI textButtonD;
    public TextMeshProUGUI costD;
    public TextMeshProUGUI textButtonA;
    public TextMeshProUGUI costA;
    public TextMeshProUGUI playerSkills;

    CharacterStats playerStats;
    string listSkill;
    int nbSkill = 0;

    private void Awake()
    {
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        playerLVL.text = "Level: " + playerStats.level.ToString();
        textButtonV.text = market.vName + " Lv." + market.GetVampirismLvl();
        costV.text = "Cost: " + market.vLvlCost.ToString();
        textButtonD.text = market.dName + " Lv." + market.GetDashLvl();
        costD.text = "Cost: " + market.dLvlCost.ToString();
        textButtonA.text = market.aName + " Lv." + market.GetAoeLvl();
        costA.text = "Cost: " + market.aLvlCost.ToString();

        while (nbSkill < playerStats.skills.Count)
        {
            listSkill += playerStats.skills[nbSkill] + "\n";
            nbSkill++;
        }

        playerSkills.text = listSkill;
    }
}
