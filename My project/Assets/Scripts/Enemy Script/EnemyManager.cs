using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player ;
    private Vector2 pPosition;
    [SerializeField] private GameObject[] enemyPrefabs;
    public bool bossPresent;
    public int totalAppeared;
    public int meleePresent;
    public int meleeKilled;
    public int rangerPresent;
    public int rangerKilled;
    public int explosivePresent;
    public int explosiveKilled;
    public int survivedWaves = 0;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pPosition = player.transform.position;
        SpawnNewEnemy();
    }

    void SpawnNewEnemy()
    {
        if (bossPresent)
        {
            
        }
        else
        {
            while (meleePresent + rangerPresent + explosivePresent < survivedWaves + 4)
            {
                if (rangerKilled >= 2)
                {
                    oocInstance(enemyPrefabs[3]);
                    meleePresent += 1;
                    rangerKilled -= 2;
                }
                
                else if (explosiveKilled >= 3)
                {
                    oocInstance(enemyPrefabs[1]);
                    explosiveKilled -= 3;
                    rangerPresent += 1;
                    Debug.Log("ranger spawned");
                }

                else if (meleeKilled >= 5)
                {
                    oocInstance(enemyPrefabs[2]);
                    meleeKilled -= 5;
                    explosivePresent += 1;
                }
                else
                {
                    oocInstance(enemyPrefabs[0]);
                    meleePresent += 1;
                }

                totalAppeared += 1;
            }

            /*for ( ; meleePresent + rangerPresent + explosiveKilled < survivedWaves +4; )
            {
                if (explosiveKilled >= 3)
                {
                    oocInstance(enemyPrefabs[1]);
                    explosiveKilled -= 3;
                    rangerPresent += 1;
                    Debug.Log("ranger spawned");
                }

                else if (meleeKilled >= 5)
                {
                    oocInstance(enemyPrefabs[2]);
                    meleeKilled -= 5;
                    explosivePresent += 1;
                }
                else
                {
                    oocInstance(enemyPrefabs[0]);
                    meleePresent += 1;
                }

                totalAppeared += 1;
            }*/
        }
    }

    void oocInstance(GameObject go)
    {
        GameObject cam = GameObject.FindWithTag("MainCamera");
        Vector2 camPos = cam.transform.position;
        float height = cam.GetComponent<Camera>().orthographicSize * 2f ;
        float Width = height * cam.GetComponent<Camera>().aspect ;
        float widthFromPlayer = Width / 2;
        float heightFromPlayer = height / 2;
        float randomValue = Random.value;
        

        int whereSpawn = Random.Range(1, 5);// 1, up / 2, left / 3, down / 4, right
        switch (whereSpawn)
        {
            case 1:
                Instantiate(go, new Vector2(randomValue * 19, 11 + camPos.y), Quaternion.identity);
                break;
            case  2:
                Instantiate(go, new Vector2(-19 + camPos.x, randomValue * 11), Quaternion.identity);
                break;
            case 3:
                Instantiate(go, new Vector2(randomValue * 19, -11 + camPos.y), Quaternion.identity);
                break;
            case 4:
                Instantiate(go, new Vector2(19 + camPos.x, randomValue * 11), Quaternion.identity);
                break;
        }
        
    }


    public void enemyKilled(int type)
    {
        switch (type)
        {
            case 0:
                meleeKilled += 1;
                meleePresent -= 1;
                break;
            case 1:
                rangerKilled += 1;
                rangerPresent -= 1;
                break;
            case 2:
                explosiveKilled = explosiveKilled + 1;
                explosivePresent = explosivePresent - 1;
                break;
        }
    }

    public void makeSpawn(bool boss)
    {
        switch (boss)
        {
            case true:
                if (bossPresent == false)
                {
                    bossPresent = true;
                    oocInstance(enemyPrefabs[3]);
                    meleePresent += 1;
                    totalAppeared += 1;
                }
                else
                {
                    
                }
                break;
            case false:
                SpawnNewEnemy();
                break;
        }
    }

    private void Update()
    {
        if (meleePresent < 0)
        {
            meleePresent = 0;
        }

        if (rangerPresent < 0)
        {
            rangerPresent = 0;
        }

        if (explosivePresent < 0)
        {
            explosivePresent = 0;
        }

        if (meleePresent + rangerPresent + explosivePresent == 0)
        {
            SpawnNewEnemy();
        }
        
    }
    
}
