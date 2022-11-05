using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject pressToEnter;
    public GameObject uiGame;
    public GameObject uiShop;


    public float heal = 10f;
    public float healDelay = 1f;
    [Header("Skill vampirism")]
    public string vName;
    public int vLvlCost;
    public float vHeal;
    public int vLvlCostScale;
    public float vHealScale;

    GameObject player;
    CharacterStats playerStats;
    Combat playerCombat;
    bool inArea = false;
    bool openShop = false;
    bool pressed = false;

    int vlevel = 1;

    private void Awake()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
            playerStats = player.GetComponent<CharacterStats>();
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
            playerStats.heal = vHeal;
            playerStats.skills.Add(vName + " Lv." + vlevel);

            vlevel++;
            vLvlCost += vLvlCostScale;
            vHeal += vHealScale;
        }
    }

    public int GetVampirismLvl()
    {
        return vlevel;
    }
}
