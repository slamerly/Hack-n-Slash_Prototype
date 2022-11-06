using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject pressToEnter;
    public GameObject uiGame;
    public GameObject uiShop;

    [Header("Skill vampirism")]
    public string vName = "Vampirism";
    public int vLvlCost = 1;
    public int vLvlCostScale = 2;
    public float vHealScale = 0.2f;

    [Header("Skill dash")]
    public string dName = "Dash";
    public int dLvlCost = 3;
    public int dLvlCostScale = 4;
    public float dDelayScale = 0.25f;
    public float dTimeScale = 0.01f;

    [Header("Skill aoe")]
    public string aName = "Aoe";
    public int aLvlCost = 3;
    public int aLvlCostScale = 4;
    public float aMultyDamageScale = 0.25f;
    public float aDelayScale = 0.25f;
    public float aRadiusScale = 1f;

    GameObject player;
    CharacterStats playerStats;
    PlayerController playerController;
    Combat playerCombat;
    bool inArea = false;
    bool openShop = false;
    bool pressed = false;

    int vlevel = 1;
    int dlevel = 1;
    int alevel = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerStats = player.GetComponent<CharacterStats>();
            playerController = player.GetComponent<PlayerController>();
            playerCombat = player.GetComponent<Combat>();
            inArea = true;
            pressToEnter.SetActive(true);
        }
    }

    private void Update()
    {
        if (Input.GetButtonDown("Shop"))
        {
            pressed = true;
            if (pressed && inArea && !openShop)
            {
                Shop();
            }

            if (pressed && openShop)
            {
                ExitShop();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            pressToEnter.SetActive(false);
            inArea = false;
        }
    }

    void Shop()
    {
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Combat>().enabled = false;
        uiGame.SetActive(false);
        uiShop.SetActive(true);
        openShop = true;
        pressed = false;
    }

    void ExitShop()
    {
        player.GetComponent<PlayerController>().enabled = true;
        player.GetComponent<Combat>().enabled = true;
        uiShop.SetActive(false);
        uiGame.SetActive(true);
        openShop = false;
        pressed = false;
    }

    public void Vampirism()
    {
        if (playerStats.level >= vLvlCost)
        {
            playerStats.level -= vLvlCost;
            playerStats.heal += vHealScale;
            playerStats.skills.Add(vName + " Lv." + vlevel);

            vlevel++;
            vLvlCost += vLvlCostScale;
        }
    }

    public void Dash()
    {
        if (playerStats.level >= dLvlCost)
        {
            playerStats.level -= dLvlCost;
            playerController.dashDelay -= dDelayScale;
            playerController.dashTime += dTimeScale;
            playerStats.skills.Add(dName + " Lv." + dlevel);

            dlevel++;
            dLvlCost += dLvlCostScale;
        }
    }

    public void Aoe()
    {
        if (playerStats.level >= aLvlCost)
        {
            playerCombat.AoeActivation(true);

            playerStats.level -= aLvlCost;
            playerCombat.aoeMultyDamage += aMultyDamageScale;
            playerCombat.aoeDelay -= aDelayScale; 
            playerCombat.aoeRadius += aRadiusScale;
            playerStats.skills.Add(aName + " Lv." + alevel);

            alevel++;
            aLvlCost += aLvlCostScale;
        }
    }

    public int GetVampirismLvl()
    {
        return vlevel;
    }

    public int GetDashLvl()
    {
        return dlevel;
    }

    public int GetAoeLvl()
    {
        return alevel;
    }
}
