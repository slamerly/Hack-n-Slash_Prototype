using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameObject target;

    private Combat player;

    // Start is called before the first frame update
    void Start()
    {
        player = target.GetComponent<Combat>();
    }

    // Update is called once per frame
    void Update()
    {
        if(player != null)
        {

        }
    }
}
