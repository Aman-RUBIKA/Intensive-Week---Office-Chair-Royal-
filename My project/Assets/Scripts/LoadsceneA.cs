using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadsceneA : MonoBehaviour
{
    public void OnButtonPress1()
    {
        Debug.Log("Bitch 1");
        SceneManager.LoadScene(1);
    }
    
    public void OnButtonPress2()
    {
        Debug.Log("Bitch 2");
        SceneManager.LoadScene(2);
    }
    public void OnButtonPress3()
    {
        Debug.Log("Bitch 3");
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

    }
    public void OnButtonPress4()
    {
        Debug.Log("Bitch 4");
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;

    }
}
    