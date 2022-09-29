using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    BASE_UPGRADE,
    UPGRADE_1,
    UPGRADE_2

}
[CreateAssetMenu(menuName = "upgradeMenu")]
public class UpgradeManager : ScriptableObject
{
    public UpgradeType upgradeType;
    public string upgradeName;
    public string upgradeID;
    public Sprite icon;
}
