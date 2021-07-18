using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInfo : MonoBehaviour
{
    public enum ItemType
    {
        Gold = 0,
        Weapon,
        GoldenGoblet = 2
    }
    public ItemType E_ItemType;
}
