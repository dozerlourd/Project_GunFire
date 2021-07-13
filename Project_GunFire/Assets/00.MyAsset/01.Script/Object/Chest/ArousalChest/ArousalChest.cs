using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArousalChest : Chest
{
    [SerializeField] GameObject goldenGoblet;

    protected override void InstantiateItem()
    {
        goldenGoblet.SetActive(true);
    }
}
