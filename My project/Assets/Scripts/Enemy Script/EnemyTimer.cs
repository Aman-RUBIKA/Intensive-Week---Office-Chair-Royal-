using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyTimer : MonoBehaviour
{
    [Header("stopwatch variables")]
    [SerializeField] private bool timerActive = true;
    public float currentTime;
    public Text currentTimeText;

    [Header("whatever else")]
    public GameObject enemyManager;
    public float timeTillBossSpawn;
    public float timeTillMinionSpawn;

    private void Update()
    {
        if (timerActive)
        {
            currentTime = currentTime + Time.deltaTime;
            if (currentTime % timeTillMinionSpawn <= 0.01f)
            {
                enemyManager.GetComponent<EnemyManager>().survivedWaves += 1;
            }
        }

        //TimeSpan time = TimeSpan.FromSeconds(currentTime);
        //currentTimeText.text = time.Minutes.ToString() + ":" + time.Seconds.ToString();
    }

    public void StartTimer()
    {
        timerActive = true;
    }

    public void StopTimer()
    {
        timerActive = true;
    }

    public void spawnEnemy()
    {
        enemyManager.GetComponent<EnemyManager>().makeSpawn(false);
    }

    public void spawnBoss()
    {
        enemyManager.GetComponent<EnemyManager>().makeSpawn(true);
    }
    
}
