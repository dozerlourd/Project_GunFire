using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldInfo : ItemInfo
{
    [SerializeField] int minGold, maxGold;
    [SerializeField] AudioClip goldClip;

    public int Gold() => Random.Range(minGold, maxGold + 1);

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "T_Player")
        {
            SoundManager.Instance.PlayOneShot(goldClip, 0.4f);
            GoldManager.SetGoldAmount(GetComponent<GoldInfo>().Gold());
            Destroy(gameObject);
        }
    }
}
