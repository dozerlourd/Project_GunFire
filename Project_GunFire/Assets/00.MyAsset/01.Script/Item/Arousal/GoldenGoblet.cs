using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenGoblet : ItemInfo
{
    public bool PressE = false;
    Coroutine co;
    [SerializeField] GameObject ArousalCanvas;
    void Start()
    {
        co = StartCoroutine(InputCheck());
        E_ItemType = ItemType.GoldenGoblet;
    }

    IEnumerator InputCheck()
    {
        yield return new WaitUntil(() => PressE);
        ArousalCanvas.SetActive(true);
        StopCoroutine(co);
    }
}
