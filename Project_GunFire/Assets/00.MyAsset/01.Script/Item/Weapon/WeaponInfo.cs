using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInfo : ItemInfo
{
    [Tooltip("현재 무기의 무기 번호 (WeaponSystem의 무기 번호 참조)"), SerializeField] int _weaponNum;
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
