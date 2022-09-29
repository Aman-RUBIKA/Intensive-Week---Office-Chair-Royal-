using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    private bool hasHalved;
    public static bool GameIsPaused = false;
    public GameObject MenuCanvas;
    public TextMeshProUGUI timerText;
    public float currentTime = 0f;
    public float randomTime;
    public bool hasLimits;
    public float timerLimit;
    public int price = 50;
    public int salePrice;
    public float priceAugment;
    public TextMeshProUGUI price1, price2, price3;
    [SerializeField] private List<UpgradeManager> upgrades;


    void Start()
    {
        MenuCanvas.SetActive(false);
        SetPrice();
        randomTime = Random.Range(1f, 10f);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }

        currentTime = randomTime -= Time.deltaTime;

        if (hasLimits && ((currentTime <= timerLimit)))
        {
            currentTime = timerLimit;
            SetTimerText();
        }

        if (currentTime<=1)
        {
            timerText.color = Color.red;
            if (hasHalved == false)
            {
                Sale();
                hasHalved = true;
            }
            else
            {
                
            }
        }
        else
        {
            timerText.color = Color.white;
        }
        SetTimerText();
        TimerReset();
    }

    private void SetTimerText()
    {
        timerText.text = currentTime.ToString();
    }
    private void SetPrice()
    {
        price1.text = price.ToString();
        price2.text = price.ToString();
        price3.text = price.ToString();
        

    }

    private void Sale()
    {
        if (currentTime<1)
        { 
            salePrice= price / 2;
            price1.text = salePrice.ToString();
            price2.text = salePrice.ToString();
            price3.text = salePrice.ToString();
        }
    }


    void Resume()
    {
        MenuCanvas.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    void Pause()
    {
        MenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("fuck");

    }

    public List<UpgradeManager> GetAvailableUpgrades(int count)
    {
        List<UpgradeManager> outputList = new List<UpgradeManager>();
        List<UpgradeManager> dupeList = new List<UpgradeManager>(upgrades);

        if (count> upgrades.Count)
        {
            count = upgrades.Count;
        }
        int range;
        for (int i = 0; i < count; i++)
        {
            range = Random.Range(0, dupeList.Count-1);  // Choses a random number between 1 and the list count
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
    private void TimerReset()
    {
        
        if (currentTime<=0)
        {
            randomTime = Random.Range(1f, 10f);
            hasHalved = false;
            SetPrice();

        }
    }

}
