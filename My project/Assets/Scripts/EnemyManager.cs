using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{

    [SerializeField] private GameObject[] enemyPrefabs;
    public int totalAppeared;
    public int meleePresent;
    private int meleeKilled;
    public int rangerPresent;
    private int rangerKilled;
    public int explosivePresent;
    private int explosiveKilled;
    public int survivedWaves;
    
    void Start()
    {
        
    }

    void SpawnNewEnemy()
    {
        if (rangerPresent < survivedWaves + 1)
        {
            Instantiate(enemyPrefabs[2]);
        }
    }
    
    
    
    void Update()
    {
        
    }
}
