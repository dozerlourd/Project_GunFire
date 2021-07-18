using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeWeapon : MonoBehaviour
{
    GameObject currWeaponTexture;
    Weapon currWeapon;

    private void Start()
    {
        StartCoroutine(ChangeWeaponTexture());
        currWeapon = WeaponSystem.Instance.CurrWeapon;
        currWeaponTexture = Instantiate(WeaponSystem.Instance.CurrWeapon.weaponTexture, transform);
        currWeaponTexture.transform.position = transform.position;
    }

    IEnumerator ChangeWeaponTexture()
    {
        while(true)
        {
            yield return new WaitUntil(() => currWeapon != WeaponSystem.Instance.CurrWeapon);
            currWeapon = WeaponSystem.Instance.CurrWeapon;
            Destroy(currWeaponTexture);
            currWeaponTexture = Instantiate(WeaponSystem.Instance.CurrWeapon.weaponTexture, transform);
            currWeaponTexture.transform.position = transform.position;
        }
    }
}
