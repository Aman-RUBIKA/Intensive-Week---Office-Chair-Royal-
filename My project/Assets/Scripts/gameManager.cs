using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public GAMESTATE currentGameState;
    public GAMESTATE desiredGameState;
    [SerializeField] Canvas             deathCanvas;

    private void Awake()
    {
        #region Simpleton
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        #endregion Simpleton
        deathCanvas.enabled = false;
    }
    private void Start()
    {
        currentGameState = GAMESTATE.PLAY;
    }

    private void Update()
    {
        switch (currentGameState)
        {
            case GAMESTATE.PAUSE:
                break;
            case GAMESTATE.PLAY:
                break;
            case GAMESTATE.GAMEOVER:
                CallWhenGameOver();
                break;
        }
    }

    public enum GAMESTATE
    {
        PLAY,
        PAUSE,
        GAMEOVER
    }

    public void CallWhenGameOver()
    {
        Time.timeScale = 0f;
        deathCanvas.enabled = true;
    }
}
