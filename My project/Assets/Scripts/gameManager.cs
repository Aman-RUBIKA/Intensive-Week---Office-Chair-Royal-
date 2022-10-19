using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class gameManager : MonoBehaviour
{
    public static gameManager instance;

    public              GAMESTATE    currentGameState, desiredGameState;
    [SerializeField]    Canvas       deathCanvas;
    public              GameObject   pausePanel;

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
        SwitchPauseAndPlay();

        switch (currentGameState)
        {
            case GAMESTATE.PAUSE:
                break;
            case GAMESTATE.PLAY:
                break;
            case GAMESTATE.GAMEOVER:
                CallWhenGameOver();
                break;
            case GAMESTATE.MAINMENU:
                break;
        }
    }

    private void SwitchPauseAndPlay()
    {
        if (InputManager.instance.pauseInput)
        {
            if (currentGameState == GAMESTATE.PLAY)
            {
                currentGameState = GAMESTATE.PAUSE;
                Capitalism.instance.Pause();
            }
            else if (currentGameState == GAMESTATE.PAUSE)
            {
                currentGameState = GAMESTATE.PLAY;
                Capitalism.instance.Resume();
            }
        }
    }

    public enum GAMESTATE
    {
        MAINMENU,
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
