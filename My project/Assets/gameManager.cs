using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GAMESTATE currentGameState;
    public GAMESTATE desiredGameState;


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
                break;
        }
    }

    public enum GAMESTATE
    {
        PLAY,
        PAUSE,
        GAMEOVER
    }
    
    
}