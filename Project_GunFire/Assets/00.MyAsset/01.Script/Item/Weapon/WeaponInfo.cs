using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : ItemInfo
{
    [Tooltip("���� ������ ���� ��ȣ (WeaponSystem�� ���� ��ȣ ����)"), SerializeField] int _weaponNum;
    public int WeaponNum => _weaponNum;
}
