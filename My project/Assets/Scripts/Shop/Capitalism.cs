using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Menu;
    public TextMeshProUGUI timerText;
    public float currentTime = 0f;
    public float randomTime;
    public bool hasLimits;
    public float timerLimit;
    
    void Start()
    {
        Menu.SetActive(false);
        randomTime = Random.Range(1f, 10f);


    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        currentTime = randomTime -= Time.deltaTime;

        if (hasLimits && ((currentTime <= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
        }

        if (currentTime<=1)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.white;
        }
        SetTimerText();
        TimerReset();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString("0.0");

    }

    void Resume()
    {
        Menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    void Pause()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("fuck");

    }
    private void TimerReset()
    {
        
        if (currentTime<=0)
        {
            randomTime = Random.Range(1f, 10f);
        }
    }

}
