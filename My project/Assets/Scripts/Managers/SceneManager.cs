using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManager : MonoBehaviour
{
    public Scene mainMenu, gameScene;
    private void Awake()
    {
        GetScenes();
    }
    void Start()
    {
        Debug.Log(gameScene.name + " and build index is " + gameScene.buildIndex);
        Debug.Log(mainMenu.name + " and build index is " + mainMenu.buildIndex);
        //SetScene(mainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetScenes()
    {
        //UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        mainMenu = UnityEngine.SceneManagement.SceneManager.GetSceneByName("Menu");
        gameScene = UnityEngine.SceneManagement.SceneManager.GetSceneByBuildIndex(0);
    }
    void SetScene(Scene scene)
    {
        if (!scene.isLoaded)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(scene.buildIndex);
        }
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(scene);
    }
}
