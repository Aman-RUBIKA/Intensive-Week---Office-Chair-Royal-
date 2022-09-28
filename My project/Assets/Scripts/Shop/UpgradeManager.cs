using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    PistolUpgrade0,
    PistolUpgrade1,
    ShotgunUpgrade0,
    ShotgunUpgrade1,
    ShotgunUpgrade2,
    MachinegunUpgrade0,
    MachinegunUpgrade1,
    MachinegunUpgrade2,

}
[CreateAssetMenu(menuName = "upgradeMenu")]
public class UpgradeManager : ScriptableObject
{
    public UpgradeType upgradeType;
    public int id;
    public static Sprite icon;
}
