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
}
    