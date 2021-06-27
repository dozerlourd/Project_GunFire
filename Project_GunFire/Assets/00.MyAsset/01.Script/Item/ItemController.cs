using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemController : MonoBehaviour
{
    protected abstract void ItemEffect();
        
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("player")) ItemEffect();
    }
}
