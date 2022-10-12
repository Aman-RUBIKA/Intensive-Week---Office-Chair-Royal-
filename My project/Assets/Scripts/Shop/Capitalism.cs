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
    public TextMeshProUGUI timerText, goldText;
    public TextMeshProUGUI price1Text, price2Text, price3Text;
    public UnityEngine.UI.Image shopImageLeft, shopImageCenter, shopImageRight;
    public UnityEngine.UI.Button shopLeft, shopCenter, shopRight;
    public List<UnityEngine.UI.Button> shopButtons;

    public float currentTime = 0f;
    public float randomTime;
    public float timerLimit;
    public static bool GameIsPaused = false;
    public static Capitalism instance;
    UpgradeManager button0Item;
    UpgradeManager button1Item;
    UpgradeManager button2Item;

    [Header("Prices Variables")]
    public float price, price1, prize2, price3 = 50;
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
        shopButtons = new List<UnityEngine.UI.Button>();
        shopButtons.Add(shopLeft);
        shopButtons.Add(shopCenter);
        shopButtons.Add(shopRight);
    }
    void Start()
    {
        MenuCanvas.enabled = false ;

        SetPrice();


        ResetShopItems();
        randomTime = Random.Range(1f, 10f);

    }

    public void ResetShopItems()
    {
        currentShopItems = GetAvailableUpgrades(3);
        //Debug.Log(currentShopItems[0]);
        //Debug.Log(currentShopItems[1]);
        //Debug.Log(currentShopItems[2]);
        if (currentShopItems[0] != null)
        {
            UpdateShopVisual(shopImageLeft, price1Text, currentShopItems[0], button0Item);
        }
        else
        {

        }
        UpdateShopVisual(shopImageCenter, price2Text, currentShopItems[1], button1Item);
        UpdateShopVisual(shopImageRight, price3Text, currentShopItems[2], button2Item);
    }
    void UpdateShopVisual(UnityEngine.UI.Image spr, TextMeshProUGUI text, UpgradeManager item, UpgradeManager list)
    {
        text.text = (PriceOutput(item.basePrice).ToString());
        spr.sprite = item.icon;
        list=item;
    }
    
    public void PriceAugment()
    {
        price += 0.2f;
    }

    int PriceOutput(float number)
    {
        return Mathf.RoundToInt(number * price) ;
    }
    
    
    public void WhichButtonWasClicked(int buttonID)
    {
        if (buttonID == 0 && GoldManager.instance.CallWhenComparingPrices(System.Convert.ToInt32(price1Text.text)))      // If This Button Is Clicked AND You Have Enough Gold
        {
            Debug.Log(System.Convert.ToInt32(price1Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(System.Convert.ToInt32(price1Text.text));
            ItemManager.instance.PlayerGetsUpgrade(currentShopItems[0]);
        }
        else if (buttonID == 1 && GoldManager.instance.CallWhenComparingPrices(System.Convert.ToInt32(price2Text.text)))
        {
            Debug.Log(System.Convert.ToInt32(price2Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(System.Convert.ToInt32(price2Text.text));
            ItemManager.instance.PlayerGetsUpgrade(currentShopItems[1]);
        }
        else if (buttonID == 2 && GoldManager.instance.CallWhenComparingPrices(System.Convert.ToInt32(price3Text.text)))
        {
            Debug.Log(System.Convert.ToInt32(price3Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(System.Convert.ToInt32(price3Text.text));
            ItemManager.instance.PlayerGetsUpgrade(currentShopItems[2]);
        }
        else
        {
            // You Don't Have Enough Money
            Debug.Log("You Are Missing Some Stolen Salary, Get Back Out There Capatalist!");
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

        if (currentTime<=0.5f)
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
            ResetShopItems();
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
        if (currentTime<0.5f)
        { 
            salePrice= Mathf.RoundToInt(price / 2);
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
        //Debug.Log("I Have Entered The Function");
        List<UpgradeManager> outputList = new List<UpgradeManager>();
        List<UpgradeManager> dupeList = new List<UpgradeManager>(ItemManager.instance.listOfUpgradeItems);
        int range;

        if (count> ItemManager.instance.listOfUpgradeItems.Count)
        {
            count = ItemManager.instance.listOfUpgradeItems.Count;
        }
        //Debug.Log(count + " And " + ItemManager.instance.listOfUpgradeItems.Count);
        for (int i = 0; i < count; i++)
        {
            range = Random.Range(0, dupeList.Count);  // Choses a random number between 1 and the list count
            //Debug.Log(range + " is Range");             
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
