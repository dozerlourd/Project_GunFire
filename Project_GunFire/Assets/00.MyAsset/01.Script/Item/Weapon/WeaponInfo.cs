using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [Tooltip("���� ������ ���� ��ȣ (WeaponSystem�� ���� ��ȣ ����)"), SerializeField] int _weaponNum;
    public int WeaponNum => _weaponNum;

    //public Weapon thisWeapon;

    //private void Start()
    //{
    //    thisWeapon = WeaponSystem.Instance.ChangeWeapon(_weaponNum);
    //}
}
