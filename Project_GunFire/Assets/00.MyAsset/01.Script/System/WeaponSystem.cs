using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weapon
{
    [Tooltip("���� �̸�")] public string weaponName;
    [Tooltip("���� ��ȣ")] public int weaponNum;
    public enum WeaponType
    {
        Hitscan = 0,
        Projective,
        Laser = 2
    }
    [Tooltip("�߻� Ÿ�� (��Ʈ��ĵ, ����ü)")] public WeaponType E_WeaponType;
    [Tooltip("���� ���ݷ�")] public float attackDamage;
    [Tooltip("�ִ� źâ ũ��")] public int maxAmmo;
    [Tooltip("���� źâ�� �Ѿ� ����")] public int currAmmo;
    [Tooltip("���������� �ɸ��� �ӵ�")] public float reloadTime;
    [Tooltip("�ʴ� ���� �ӵ� (0.01 ~ 20.0)"), Range(0.01f, 20.0f)] public float rateOfFire;
    [Tooltip("ġ��Ÿ Ȯ�� (0 ~ 100)"), Range(0.01f, 100.0f)] public float criticalRate;
    [Tooltip("ġ��Ÿ ��� (100.0f ~ 500.0f)"), Range(100.0f, 500.0f)] public float criticalMultiflier;
    [Tooltip("�̵��ӵ� ������ (-100.0f ~ 100.0f)"), Range(-100.0f, 100.0f)] public float moveSpeedRate;
    [Tooltip("ź���� ��ġ - Hitscan ���⸸ ��� (?f ~ ?f)"), /*Range(-100.0f, 100.0f)*/] public float fireSpreadRate;
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
            currWeapon.currAmmo = currWeapon.maxAmmo;
        }
    }
    [SerializeField] private Weapon currWeapon;

    private void Awake()
    {
        CurrWeapon = ChangeWeapon(Weapons[0].weaponNum);
    }

    /// <summary> ���� ���� ���� ������ ������ ������ ������ ������ ������Ʈ �����ִ� �Լ� </summary>
    /// <param name="num"> 1��: Foundry, 2��: Laser Gauntlet, 3��: Aura of Venom, 4��: Crimson Firescale </param>
    /// <returns> num�� ���� �ش�Ǵ� ���⸦ ��ȯ�Ѵ�. </returns>
    public Weapon ChangeWeapon(int num) => CurrWeapon = weapons[num - 1001];


}