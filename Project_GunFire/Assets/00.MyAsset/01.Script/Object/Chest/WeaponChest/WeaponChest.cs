using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChest : Chest
{
    [SerializeField] List<GameObject> Weapons = new List<GameObject>();
    [SerializeField] Transform[] initTr;
    protected override void InstantiateItem()
    {
        
        for (int i = 0; i < 3; i++)
        {
            int temp = Random.Range(0, Weapons.Count);
            Instantiate(Weapons[temp], initTr[i].position, Quaternion.identity);
            Weapons.RemoveAt(temp);
        }
    }
}
