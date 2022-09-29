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
        //positionsOfStartingItems = new List<int>();
        //positionsOfStartingItems.Add(0, 2, 5)
    }
    void Start()
    {
        if (listOfPlayerItems != null)
        {
            listOfPlayerItems = null;
        }
        AddUpgradeItemsOnStart();
        listOfPlayerItems = new List<UpgradeManager>();
    }

    void Update()
    {
        
    }
    public void PlayerGetsUpgrade(UpgradeManager item)      //Call This Whenever The Player Gets An Upgrade
    {
        int removeIndex;
        string upgradeIdInitial = item.upgradeID.ToString();
        upgradeIdInitial = upgradeIdInitial[0].ToString();
        removeIndex = listOfUpgradeItems.FindInstanceID(item);

        if (item.upgradeType != UpgradeType.UPGRADE_2)
        {
            // To Get The First Letter Of The String
            Debug.Log(removeIndex + " is Remove Index and Item is " + item);
            // Add It To Player Inventory
            listOfUpgradeItems.Add(listOfAllItems[FindItemInList(item, listOfAllItems) + 1]);                 // Increment The Shop Rewards (If It Can Happen)

        }
        listOfUpgradeItems.RemoveAt(removeIndex);                                           //Remove It From The List Of Available Upgrades
        listOfPlayerItems.Add(item); 
        /*else (listOfUpgradeItems[removeIndex].upgradeID.Contains(upgradeIdInitial))  
        {

        }*/
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
