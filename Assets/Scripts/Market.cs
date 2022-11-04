using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public GameObject pressToEnter;
    public GameObject uiGame;
    public GameObject uiShop;

    GameObject player;
    bool inArea = false;
    bool openShop = false;
    bool pressed = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.gameObject;
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
                //player.GetComponent<PlayerController>().enabled = false;
                Debug.Log("press B");
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
        Time.timeScale = 0f;
        uiGame.SetActive(false);
        uiShop.SetActive(true);
        openShop = true;
        pressed = false;
    }

    void ExitShop()
    {
        player.GetComponent<PlayerController>().enabled = true;
        Time.timeScale = 1f;
        uiShop.SetActive(false);
        uiGame.SetActive(true);
        openShop = false;
        pressed = false;
    }
}
