using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldManager : MonoBehaviour
{
    public static GoldManager instance;
    [SerializeField]
    private float currentGold;
    public TextMeshProUGUI textGold;

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
    }
    public void AddGold(int acquired)
    {
        currentGold += acquired;
        textGold.text = "$ " + currentGold.ToString();
        Debug.Log(currentGold);
    }


    public void CallWhenBought(int goldremoval)
    {
        currentGold -= goldremoval;
        textGold.text = "$ :" + currentGold.ToString();
    }

    public bool CallWhenComparingPrices(int price)
    {
        if (price > currentGold)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    
    
}
