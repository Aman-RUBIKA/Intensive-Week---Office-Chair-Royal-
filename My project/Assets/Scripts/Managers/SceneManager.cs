using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Scene mainMenu, gameScene;
    // Start is called before the first frame update
    void Start()
    {
        SetScenes();
        Debug.Log(gameScene.name + " and build index is " + gameScene.buildIndex);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void SetScenes()
    {
        //mainMenu.name = ;
        gameScene.name = "Explosive Playground";
        gameScene = UnityEngine.SceneManagement.SceneManager.GetSceneAt(1);
    }
}
