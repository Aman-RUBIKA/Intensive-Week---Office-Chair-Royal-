using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    public static bool GameIsPaused = false;
    public GameObject Menu;
    
    void Start()
    {
        Menu.SetActive(false);

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

}
