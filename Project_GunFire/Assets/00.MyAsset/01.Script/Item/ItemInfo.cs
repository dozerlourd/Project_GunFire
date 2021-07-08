using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemInfo : MonoBehaviour
{
    [SerializeField] protected float gravityScale;

    public enum ItemType
    {
        Gold = 0,
        Weapon,
        Scroll = 2
    }
    [HideInInspector] public ItemType E_ItemType;

    protected abstract float GravityScale();
    protected abstract Vector3 GravityDirection();
}
