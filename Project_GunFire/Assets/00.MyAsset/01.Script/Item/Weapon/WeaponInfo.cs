using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : MonoBehaviour
{
    [Tooltip("현재 무기의 무기 번호 (WeaponSystem의 무기 번호 참조)"), SerializeField] int _weaponNum;
    public int WeaponNum => _weaponNum;

    //public Weapon thisWeapon;

    //private void Start()
    //{
    //    thisWeapon = WeaponSystem.Instance.ChangeWeapon(_weaponNum);
    //}
}
