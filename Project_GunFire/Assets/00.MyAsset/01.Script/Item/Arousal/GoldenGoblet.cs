using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldenGoblet : ItemInfo
{
    Coroutine co;
    [SerializeField] GameObject ArousalCanvas;
    void Start()
    {
        co = StartCoroutine(InputCheck());
    }

    IEnumerator InputCheck()
    {
        yield return new WaitUntil(() => PressE);
        ArousalCanvas.SetActive(true);
        StopCoroutine(co);
    }

    protected override float GravityScale() => 0;

    protected override Vector3 GravityDirection() => Vector3.zero;
}
