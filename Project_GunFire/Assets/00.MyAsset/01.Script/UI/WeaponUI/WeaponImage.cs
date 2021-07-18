using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponImage : MonoBehaviour
{
    Image currWeaponImage;
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    private void Start()
    {
        currWeaponImage = GetComponent<Image>();
        StartCoroutine(Co_AmmoCheck());
    }

    IEnumerator Co_AmmoCheck()
    {
        while (true)
        {
            currWeaponImage.sprite = WeaponSystem.Instance.CurrWeapon.weaponSprite;
            yield return waitForEndOfFrame;
        }
    }
}
