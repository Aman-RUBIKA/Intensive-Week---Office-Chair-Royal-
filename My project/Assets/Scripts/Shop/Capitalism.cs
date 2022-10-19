using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using System.Linq;

public class Capitalism : MonoBehaviour
{
    [Header("UI Elements")]
    public Canvas                       MainCanvas;
    public GameObject                   shopPanel, hudPanel, goldPanel;
    public TextMeshProUGUI              timerText, goldText;
    public TextMeshProUGUI              price1Text, price2Text, price3Text;
    public UnityEngine.UI.Image         shopImageLeft, shopImageCenter, shopImageRight;
    public UnityEngine.UI.Button        shopLeft, shopCenter, shopRight;
    public List<UnityEngine.UI.Button>  shopButtons;

    public  float                currentTime         = 0f;
    public  float                randomTime;
    public  float                timerLimit;
    public  static bool          GameIsPaused        = false;
    public  static Capitalism    instance;
    private UpgradeManager       button0Item, button1Item, button2Item;
    public  UpgradeManager       empty;
    public  string               outOfStockText      = "Out Of Stock";

    [Header("Prices Variables")]
    public              float                           price, price1, prize2, price3 = 50;
    public              int                             salePrice;
    private             bool                            hasHalved;
    public              float                           priceAugment;
    [SerializeField]    List<UpgradeManager>            upgrades;
    public              List<UpgradeManager>            currentShopItems;


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
        shopButtons.Add(UiManager.instance.shopLeft);
        shopButtons.Add(UiManager.instance.shopCenter);
        shopButtons.Add(UiManager.instance.shopRight);
    }
    void Start()
    {

        SetPrice();


        ResetShopItems();
        randomTime = Random.Range(1f, 10f);
        if (gameManager.instance.currentGameState == gameManager.GAMESTATE.PLAY)
        {
            GameIsPaused = false;
        }
        else
        {
            GameIsPaused = true;
        }

    }

    public void ResetShopItems()
    {
        currentShopItems = GetAvailableUpgrades(3);
        //Debug.Log(currentShopItems[0]);
        //Debug.Log(currentShopItems[1]);
        //Debug.Log(currentShopItems[2]);

        if (currentShopItems.Count>=3)
        {
            UpdateShopVisual(UiManager.instance.shopImageRight, UiManager.instance.price3Text, currentShopItems[2], button0Item);
        }
        else
        {
            UpdateShopVisual(UiManager.instance.shopImageRight, UiManager.instance.price3Text, button2Item, true);
        }

        if (currentShopItems.Count>=2)
        {
            UpdateShopVisual(UiManager.instance.shopImageCenter, price2Text, currentShopItems[1], button1Item);
        }
        else
        {
            UpdateShopVisual(UiManager.instance.shopImageCenter, price2Text, button1Item, true);
        }

        if (currentShopItems.Count >=1)
        {
            UpdateShopVisual(UiManager.instance.shopImageLeft, UiManager.instance.price1Text, currentShopItems[0], button0Item);
        }
        else
        {
            UpdateShopVisual(UiManager.instance.shopImageLeft, UiManager.instance.price1Text, button0Item, true);
        }
    }
    void UpdateShopVisual(UnityEngine.UI.Image spr, TextMeshProUGUI text, UpgradeManager item, UpgradeManager list)
    {
        text.text = (PriceOutput(item.basePrice).ToString());
        spr.sprite = item.icon;
        list=item;
    }
    void UpdateShopVisual(UnityEngine.UI.Image spr, TextMeshProUGUI text, UpgradeManager item, bool isEmpty)
    {
        if (isEmpty)
        {
            text.text = outOfStockText;
            spr.sprite = empty.icon;
            item = empty;
        }
    }
    
    public void PriceAugment()
    {
        price += 0.5f;
    }

    int PriceOutput(float number)
    {
        return Mathf.RoundToInt(number * price) ;
    }
    
    
    public void WhichButtonWasClicked(int buttonID)
    {
        int price1 = ConvertShopTextToInt(UiManager.instance.price1Text.text);
        Debug.Log(price1);
        int price2 = ConvertShopTextToInt(UiManager.instance.price2Text.text);
        Debug.Log(price2);
        int price3 = ConvertShopTextToInt(UiManager.instance.price3Text.text);
        Debug.Log(price3);

        if (buttonID == 0 && price1 !=-1 && GoldManager.instance.CallWhenComparingPrices(price1))      // If This Button Is Clicked AND You Have Enough Gold. It Retuns -1 If The Position In Shop Is Out Of Stock
        {
            Debug.Log(System.Convert.ToInt32(UiManager.instance.price1Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(price1);
            ItemManager.instance.PlayerGetsUpgrade(currentShopItems[0]);
        }
        else if (buttonID == 1 && price2!=-1 && GoldManager.instance.CallWhenComparingPrices(price2))
        {
            Debug.Log(System.Convert.ToInt32(UiManager.instance.price2Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(price2);
            ItemManager.instance.PlayerGetsUpgrade(currentShopItems[1]);
        }
        else if (buttonID == 2 && price3!=-1 && GoldManager.instance.CallWhenComparingPrices(price3))
        {
            Debug.Log(System.Convert.ToInt32(UiManager.instance.price3Text.text) + " is price of this item.");
            GoldManager.instance.CallWhenBought(price3);
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



        if (currentTime <= timerLimit)
        {
            currentTime = timerLimit;
            SetTimerText();
        }

        if (currentTime<=0.5f)
        {
            if (UiManager.instance.timerText.color != Color.red)
            {
                UiManager.instance.timerText.color = Color.red;
            }
            if (hasHalved == false)
            {
                Sale();
                hasHalved = true;
            }
        }
        else
        {
            if (UiManager.instance.timerText.color != Color.white)
            {
                UiManager.instance.timerText.color = Color.white;
            }
        }
        SetTimerText();
        TimerReset();
    }

    private int ConvertShopTextToInt(string text)
    {
        if (text != outOfStockText)
        {
            return System.Convert.ToInt32(text);
        }
        else return -1;                         // Retuns -1, if out of stock, because this value should normally not be less than 0
    }
    private void SetTimerText()
    {
        UiManager.instance.timerText.text = currentTime.ToString();
    }
    private void TimerReset()
    {

        if (currentTime <= 0)
        {
            randomTime = Random.Range(20f, 45f);
            hasHalved = false;
            ResetShopItems();
        }
    }
    private void SetPrice()
    {
        UiManager.instance.price1Text.text = price.ToString();
        UiManager.instance.price2Text.text = price.ToString();
        UiManager.instance.price3Text.text = price.ToString();
    }

    private void Sale()
    {
        if (currentTime<0.5f)
        {
            /*int newPrice1;
            int newPrice2;
            int newPrice3;
            newPrice1 = System.Convert.ToInt32(UiManager.instance.price1Text);
            newPrice2 = System.Convert.ToInt32(UiManager.instance.price2Text);
            newPrice3 = System.Convert.ToInt32(UiManager.instance.price3Text);
            Debug.Log(newPrice1 + " is new price 1");
            newPrice1 = Mathf.RoundToInt(newPrice1 / 2);
            newPrice2 = Mathf.RoundToInt(newPrice2 / 2);
            newPrice3 = Mathf.RoundToInt(newPrice3 / 2);

            UiManager.instance.price1Text.text = newPrice1.ToString();
            UiManager.instance.price2Text.text = newPrice2.ToString();
            UiManager.instance.price3Text.text = newPrice3.ToString(); */
            salePrice = Mathf.RoundToInt(price / 2);

            UiManager.instance.price1Text.text = salePrice.ToString();
            UiManager.instance.price2Text.text = salePrice.ToString();
            UiManager.instance.price3Text.text = salePrice.ToString();
        }
    }
    
    #region Pause And Resume
    public void Resume()
    {
        MainCanvas.enabled = false;
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        MainCanvas.enabled = true;
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
