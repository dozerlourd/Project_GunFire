using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : ItemInfo
{
    [Tooltip("���� ������ ���� ��ȣ (WeaponSystem�� ���� ��ȣ ����)"), SerializeField] int _weaponNum;
    public int WeaponNum => _weaponNum;

    protected override float GravityScale()
    {
        return 0;
    }

    protected override Vector3 GravityDirection()
    {
        return Vector3.down;
    }
}
