using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InfoAndUI
{
    [Tooltip("WeaponSystem의 WeaponNum과 비교하여 같은 무기인지 확인하기 위한 무기 번호")] public int weaponNum;
    [Tooltip("화면에 띄울 무기의 정보를 담은 UI")] public Sprite WeaponInfoUIImage;
}

public class WeaponInfoUISystem : SceneObject<WeaponInfoUISystem>
{
    [SerializeField] InfoAndUI[] arr_WeaponInfoUI;
    public InfoAndUI[] Arr_WeaponInfoUI => arr_WeaponInfoUI;
}
