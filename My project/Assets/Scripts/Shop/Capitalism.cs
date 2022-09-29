using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Capitalism : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas MenuCanvas;
    public TextMeshProUGUI timerText;
    public TextMeshProUGUI price1Text, price2Text, price3Text;
    public UnityEngine.UI.Image shopImageLeft, shopImageCenter, shopImageRight;
    public UnityEngine.UI.Button shopLeft, shopCenter, shopRight;

    public float currentTime = 0f;
    public float randomTime;
    public float timerLimit;
    public static bool GameIsPaused = false;
    public static Capitalism instance;

    [Header("Prices Variables")]
    public int price = 50;
    public int salePrice;
    private bool hasHalved;
    public float priceAugment;
    [SerializeField] 
    private List<UpgradeManager> upgrades;
    public List<UpgradeManager> currentShopItems;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {
        MenuCanvas.enabled = false ;

        SetPrice();

        

        randomTime = Random.Range(1f, 10f);

    }

    public void ResetShopItems()
    {
        currentShopItems = GetAvailableUpgrades(3);
        Debug.Log(currentShopItems[0]);
        foreach (UpgradeManager item in currentShopItems)
        {
            Debug.Log(item.name);
        }
    }
    // Update is called once per frame
    void Update()
    {
        currentTime = randomTime -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            PauseResume();
        }


        if (currentTime <= timerLimit)
        {
            currentTime = timerLimit;
            SetTimerText();
        }

        if (currentTime<=1)
        {
            if (timerText.color != Color.red)
            {
                timerText.color = Color.red;
            }
            if (hasHalved == false)
            {
                Sale();
                hasHalved = true;
            }
        }
        else
        {
            if (timerText.color != Color.white)
            {
                timerText.color = Color.white;
            }
        }
        SetTimerText();
        TimerReset();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString();
    }
    private void TimerReset()
    {

        if (currentTime <= 0)
        {
            randomTime = Random.Range(1f, 10f);
            hasHalved = false;

            
            SetPrice();
        }
    }
    private void SetPrice()
    {
        price1Text.text = price.ToString();
        price2Text.text = price.ToString();
        price3Text.text = price.ToString();
    }

    private void Sale()
    {
        if (currentTime<1)
        { 
            salePrice= price / 2;
            price1Text.text = salePrice.ToString();
            price2Text.text = salePrice.ToString();
            price3Text.text = salePrice.ToString();
        }
    }
    
    #region Pause And Resume
    void Resume()
    {
        MenuCanvas.enabled = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void Pause()
    {
        MenuCanvas.enabled = true;
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("fuck");
    }

    void PauseResume()
    {
        if (GameIsPaused)
        {
            Resume();
        }
        else { Pause(); }
    }
    #endregion Pause And Resume


    public List<UpgradeManager> GetAvailableUpgrades(int count)
    {
        Debug.Log("I Have Entered The Function");
        List<UpgradeManager> outputList = new List<UpgradeManager>();
        List<UpgradeManager> dupeList = new List<UpgradeManager>(ItemManager.instance.listOfUpgradeItems);
        int range;

        if (count> ItemManager.instance.listOfUpgradeItems.Count)
        {
            count = ItemManager.instance.listOfUpgradeItems.Count;
        }
        Debug.Log(count + " And " + ItemManager.instance.listOfUpgradeItems.Count);
        for (int i = 0; i < count; i++)
        {
            range = Random.Range(0, dupeList.Count);  // Choses a random number between 1 and the list count
            Debug.Log(range + " is Range");             
            outputList.Add(dupeList[range]);            // Adds This Item To The Output List
            dupeList.Remove(dupeList[range]);           // Removes It So It Cannot be added again, in the same cycle.
        }

        dupeList.Clear();
        return outputList;
    }
    public void DisplayUpgrades()
    {
        Debug.Log("I Have Been Clicked Bwahaha");
        List<UpgradeManager> upgradesToShow = GetAvailableUpgrades(3); 
        for (int i=0; i<=2; i++)
        {
            Debug.Log("Initiating Spin");
            Debug.Log(upgradesToShow[i]);
        }
    }
    

}
