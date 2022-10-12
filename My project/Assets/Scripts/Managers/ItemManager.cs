using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager instance;
    public List<UpgradeManager> listOfAllItems ;
    public List<UpgradeManager> listOfUpgradeItems, listOfPlayerItems, listChosenForShop;

    public List<int> positionsOfStartingItems ;

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
        ClearLists();
        AddUpgradeItemsOnStart();
        //positionsOfStartingItems = new List<int>();
        //positionsOfStartingItems.Add(0, 2, 5)
    }
    void Start()
    {
        if (listOfPlayerItems != null)
        {
            listOfPlayerItems = null;
        }
        
        listOfPlayerItems = new List<UpgradeManager>();
    }

    void Update()
    {
        
    }
    public void PlayerGetsUpgrade(UpgradeManager item)      //Call This Whenever The Player Gets / Buys An Upgrade
    {
        int removeIndex;
        //string upgradeIdInitial = item.upgradeID.ToString();
        //upgradeIdInitial = upgradeIdInitial[0].ToString();
        //Debug.Log(item);
        //Debug.Log(listOfUpgradeItems.FindInstanceID<UpgradeManager>(item));
        removeIndex = listOfUpgradeItems.FindInstanceID(item);

        if (item.upgradeType != UpgradeType.UPGRADE_2)
        {
            Debug.Log(removeIndex + " is Remove Index and Item is " + item);
            listOfUpgradeItems.Add(listOfAllItems[FindItemInList(item, listOfAllItems) + 1]);                   // Increment The Shop Rewards (If It Can Happen)
        }
        listOfUpgradeItems.RemoveAt(removeIndex);                                                               // Remove It From The List Of Available Upgrades
        WhenPlayerGetsUpgrade(item); // Add Item To Player's Inventory
        Capitalism.instance.PriceAugment();
        //Capitalism.instance.

        /*else (listOfUpgradeItems[removeIndex].upgradeID.Contains(upgradeIdInitial))  
        {

        }*/
    }
    public void WhenPlayerGetsUpgrade(UpgradeManager item)      // Enables The Upgrade If The Player Acquires It
    {
        listOfPlayerItems.Add(item);
        string compareID = item.upgradeID[0].ToString();
        UpgradeType upgradeLevel = item.upgradeType;
        //UpgradeType level0 = UpgradeType.BASE_UPGRADE;
        UpgradeType level1 = UpgradeType.UPGRADE_1;
        UpgradeType level2 = UpgradeType.UPGRADE_2;
        switch (compareID)
        {
            case "A":
                if (upgradeLevel == level2)
                {
                    ShootManager.instance.pistol2 = true;
                }
                else if (upgradeLevel== level1)
                {
                    ShootManager.instance.pistol1 = true;
                }
                else
                {
                    ShootManager.instance.pistol0 = true;
                }
                break;
            case "B":
                if (upgradeLevel == level2)
                {
                    ShootManager.instance.mach2 = true;
                }
                else if (upgradeLevel == level1)
                {
                    ShootManager.instance.mach1 = true;
                }
                else
                {
                    ShootManager.instance.mach0 = true;
                }
                break;
            case "C":
                if (upgradeLevel == level2)
                {
                    ShootManager.instance.shotgun2 = true;
                }
                else if (upgradeLevel == level1)
                {
                    ShootManager.instance.shotgun1 = true;
                }
                else
                {
                    ShootManager.instance.shotgun0 = true;
                }
                break;
                
        }
        Capitalism.instance.ResetShopItems();
    }
    public int FindItemInList(UpgradeManager item, List<UpgradeManager> shopList)
    {
        return shopList.FindInstanceID(item);
    }
    void ClearLists()
    {
        listOfPlayerItems.Clear();
        listOfUpgradeItems.Clear();
    }
    void AddUpgradeItemsOnStart()
    {
        for (int i = 0; i <= positionsOfStartingItems.Count-1; i++)
        {
            //Debug.Log(positionsOfStartingItems[i]);
            //Debug.Log(listOfAllItems[positionsOfStartingItems[i]]);
            listOfUpgradeItems.Add(listOfAllItems[positionsOfStartingItems[i]]);
        }
    }
}
