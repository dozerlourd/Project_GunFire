using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Tooltip("무기 이름")] public string weaponName;
    [Tooltip("무기 번호")] public string weaponNum;
    public enum WeaponType
    {
        Hitscan = 0,
        Projective = 1
    }
    [Tooltip("발사 타입 (히트스캔, 투사체)")] public WeaponType E_WeaponType;
    [Tooltip("무기 공격력")] public float attackDamage;
    [Tooltip("최대 탄창 크기")] public float maxAmmo;
    [Tooltip("재장전까지 걸리는 속도")] public float reloadTime;
    [Tooltip("초당 공격 속도 (0.01 ~ 20.0)"), Range(0.01f, 20.0f)] public float rateOfFire;
    [Tooltip("치명타 확률 (0 ~ 100)"), Range(0.01f, 100.0f)] public float criticalRate;
    [Tooltip("치명타 배수 (100.0f ~ 500.0f)"), Range(100.0f, 500.0f)] public float criticalMultiflier;
    [Tooltip("이동속도 증감률 (-100.0f ~ 100.0f"), Range(-100.0f, 100.0f)] public float moveSpeedRate;
}

public class WeaponSystem : SceneObject<WeaponSystem>
{
    public Weapon[] Weapons => weapons;
    [SerializeField] Weapon[] weapons;

    public Weapon CurrWeapon
    {
        get => currWeapon; set
        {
            currWeapon = value;
        }
    }
    [SerializeField] private Weapon currWeapon;

    private void Start()
    {
        CurrWeapon = ChangeWeapon(0);
    }

    /// <summary> 현재 착용 중인 무기의 정보를 변경할 무기의 정보로 업데이트 시켜주는 함수 </summary>
    /// <param name="num"> 1번: Foundry, 2번: Laser Gauntlet, 3번: Aura of Venom, 4번: Crimson Firescale </param>
    /// <returns> num의 값에 해당되는 무기를 반환한다. </returns>
    public Weapon ChangeWeapon(int num) => CurrWeapon = weapons[num];


}
