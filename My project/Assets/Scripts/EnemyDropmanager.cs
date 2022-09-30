using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyDropmanager : MonoBehaviour
{
    public GameObject prefab;
    public bool canAppearItem = false;
    public bool hasItemAppeared = false;
    [SerializeField] float itemTimer;
    public float maxItemTimer;

    private void Start()
    {
        itemTimer = 0;
    }

    void Update()
    {
        if (itemTimer >= maxItemTimer)
        {
            canAppearItem = true;
        }
        else
        {
            itemTimer += Time.deltaTime;
            canAppearItem = false;
        }
    }
    
    public void appearFood(Vector2 pos)
    {
        
        if (canAppearItem && hasItemAppeared == false && itemTimer >= maxItemTimer)
        {
            hasItemAppeared = true;
            
            Instantiate(prefab, pos, quaternion.identity);
        }
    }
    
}


