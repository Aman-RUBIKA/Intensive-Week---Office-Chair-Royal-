using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private GameObject player ;
    private Vector2 pPosition;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private bool BossPresent;
    public int totalAppeared;
    public int meleePresent;
    private int meleeKilled;
    public int rangerPresent;
    private int rangerKilled;
    public int explosivePresent;
    private int explosiveKilled;
    public int survivedWaves = 0;
    
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        pPosition = player.transform.position;
        SpawnNewEnemy();
    }

    void SpawnNewEnemy()
    {
        if (BossPresent)
        {
            
        }
        else
        {
            while (meleePresent + rangerPresent + explosivePresent < survivedWaves + 4)
            {
                if (explosiveKilled >= 3)
                {
                    oocInstance(enemyPrefabs[3]);
                    explosiveKilled -= 3;
                    rangerPresent += 1;
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
            } 
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
        float randomvalue = Random.value;
        

        int whereSpawn = Random.Range(1, 5);// 1, up / 2, left / 3, down / 4, right
        switch (whereSpawn)
        {
            case 1:
                Instantiate(go, new Vector2(randomvalue * 19, 11 + camPos.y), Quaternion.identity);
                break;
            case  2:
                Instantiate(go, new Vector2(-19 + camPos.x, randomvalue * 11), Quaternion.identity);
                break;
            case 3:
                Instantiate(go, new Vector2(randomvalue * 19, -11 + camPos.y), Quaternion.identity);
                break;
            case 4:
                Instantiate(go, new Vector2(19 + camPos.x, randomvalue * 11), Quaternion.identity);
                break;
        }
        Debug.Log(Width);
        Debug.Log(height);
        
    }
    
    
    void Update()
    {
        
    }
}
