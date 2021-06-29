using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLootingWeapon : MonoBehaviour
{
    Vector3 rayOriginVec, rayDirectionVec;
    [SerializeField] float rayDist;

    [Space(15)]
    [SerializeField] GameObject weaponInfoUICanvas;
    [SerializeField] Image weaponInfoUIImage;
    private Image WeaponInfoUIImage
    {
        get
        {
            weaponInfoUIImage.color = ray ? Color.white : Color.clear;
            return weaponInfoUIImage;
        }
    }
    [SerializeField] float UIPlusY;
    [SerializeField] bool ray;

    void Update()
    {
        RaycastHit rayHitInfo;
        SetrayVectorInfo();

        ray = Physics.Raycast(rayOriginVec, rayDirectionVec, out rayHitInfo, rayDist, LayerMask.GetMask("L_Weapon"));

        if (ray)
        {
            int weaponNum = rayHitInfo.collider.GetComponent<WeaponInfo>().WeaponNum;
            Vector3 UIPos = rayHitInfo.collider.transform.position + Vector3.up * UIPlusY;
            InfoAndUI InfoAndUI = WeaponInfoUISystem.Instance.Arr_WeaponInfoUI[weaponNum - 1001];

            WeaponInfoUIImage.sprite = weaponNum == InfoAndUI.weaponNum ? InfoAndUI.WeaponInfoUIImage : null;
            //weaponInfoUICanvas.transform.position = UIPos;
            //weaponInfoUICanvas.transform.position = transform.position + transform.forward * 0.3f + transform.up * 0.05f;
        }
        else
        {
            WeaponInfoUIImage.sprite = null;
        }
    }

    void SetrayVectorInfo()
    {
        rayOriginVec = Camera.main.transform.position;
        rayDirectionVec = Camera.main.transform.forward;
    }
}
