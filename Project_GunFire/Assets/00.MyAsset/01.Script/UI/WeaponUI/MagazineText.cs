using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagazineText : MonoBehaviour
{
    Text magazineText;
    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    int _currAmmo => WeaponSystem.Instance.CurrWeapon.currAmmo;
    int _maxAmmo => WeaponSystem.Instance.CurrWeapon.maxAmmo;

    private void Start()
    {
        magazineText = GetComponent<Text>();
        StartCoroutine(Co_AmmoCheck());
    }

    IEnumerator Co_AmmoCheck()
    {
        while(true)
        {
            magazineText.text = string.Format("{0} / {1}", _currAmmo, _maxAmmo);
            yield return waitForEndOfFrame;
        }
    }
}
