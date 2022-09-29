using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public float currentGold;
    public Text textGold;

    public void AddGold(int acquired)
    {
        currentGold += acquired;
        textGold.text = "$ :" + currentGold.ToString();
        Debug.Log(currentGold);

    }
    
    
    
    
}
