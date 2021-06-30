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
    Image weaponInfoUIImage;
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

    private void Start()
    {
        weaponInfoUIImage = weaponInfoUICanvas.transform.GetChild(0).GetComponent<Image>();
    }

    void Update()
    {
        SetRayVectorInfo();
        DisplayWeaponUI();
    }

    void SetRayVectorInfo()
    {
        rayOriginVec = Camera.main.transform.position;
        rayDirectionVec = Camera.main.transform.forward;
    }

    void DisplayWeaponUI()
    {
        RaycastHit rayHitInfo;

        ray = Physics.Raycast(rayOriginVec, rayDirectionVec, out rayHitInfo, rayDist, LayerMask.GetMask("L_Weapon"));

        if (ray)
        {
            int weaponNum = rayHitInfo.collider.GetComponent<WeaponInfo>().WeaponNum;
            Vector3 UIPos = rayHitInfo.collider.transform.position + Vector3.up * UIPlusY;
            InfoAndUI InfoAndUI = WeaponInfoUISystem.Instance.Arr_WeaponInfoUI[weaponNum - 1001];

            WeaponInfoUIImage.sprite = weaponNum == InfoAndUI.weaponNum ? InfoAndUI.WeaponInfoUIImage : null;
            weaponInfoUICanvas.transform.position = UIPos;
<<<<<<< HEAD
            weaponInfoUICanvas.transform.position = rayHitInfo.collider.transform.position
                + rayHitInfo.collider.transform.up * UIPlusY;
=======
            //weaponInfoUICanvas.transform.position = transform.position + transform.forward * 0.3f + transform.up * 0.05f;

            if(Input.GetKeyDown(KeyCode.F)) WeaponSystem.Instance.ChangeWeapon(weaponNum);
>>>>>>> HCH_Branch
        }
        else
        {
            WeaponInfoUIImage.sprite = null;
        }
    }
}
