using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InfoAndUI
{
    [Tooltip("WeaponSystem�� WeaponNum�� ���Ͽ� ���� �������� Ȯ���ϱ� ���� ���� ��ȣ")] public int weaponNum;
    [Tooltip("ȭ�鿡 ��� ������ ������ ���� UI")] public Sprite WeaponInfoUIImage;
}

public class WeaponInfoUISystem : SceneObject<WeaponInfoUISystem>
{
    [SerializeField] InfoAndUI[] arr_WeaponInfoUI;
    public InfoAndUI[] Arr_WeaponInfoUI => arr_WeaponInfoUI;
}
