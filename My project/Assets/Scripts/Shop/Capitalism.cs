using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Capitalism : MonoBehaviour
{
    private bool hasHalved;
    public static bool GameIsPaused = false;
    public GameObject Menu;
    public TextMeshProUGUI timerText;
    public float currentTime = 0f;
    public float randomTime;
    public bool hasLimits;
    public float timerLimit;
    public int price = 50;
    public int salePrice;
    public float priceAugment;
    public TextMeshProUGUI price1;
    public TextMeshProUGUI price2;
    public TextMeshProUGUI price3;
    [SerializeField] private List<UpgradeManager> upgrades;


    void Start()
    {
        Menu.SetActive(false);
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
        timerText.text = currentTime.ToString("0.0");
        

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
        Menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;


    }

    void Pause()
    {
        Menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
        Debug.Log("fuck");

    }

    public List<UpgradeManager> GetUpgrades(int count)
    {
        List<UpgradeManager> upgradeList = new List<UpgradeManager>();

        if (count> upgrades.Count)
        {
            count = upgrades.Count;
        }
        
        for (int i = 0; i < count; i++)
        {
            upgradeList.Add(upgrades[Random.Range(0, upgrades.Count)]);
        }

        return upgradeList;
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
