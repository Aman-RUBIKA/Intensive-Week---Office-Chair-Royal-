using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class food : MonoBehaviour
{
    private GameObject player;
    private GameObject foodManager;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        foodManager = GameObject.FindWithTag("EnemyDropManager");
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            foodManager.GetComponent<EnemyDropmanager>().hasItemAppeared = false;
            player.GetComponent<HealthPC>().CallWhenHealed();
            Destroy(this.gameObject); 
        }
        
    }
}
