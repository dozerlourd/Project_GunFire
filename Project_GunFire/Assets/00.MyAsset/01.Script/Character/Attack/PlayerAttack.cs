using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    WeaponSystem weaponSystem;

<<<<<<< HEAD
    public float CoolTime
    {
        get => coolTime; set => coolTime = 1 / WeaponSystem.Instance.CurrWeapon.rateOfFire;
    }
    [SerializeField] float coolTime = 0.35f;
=======
    /// <summary> ������ �ʴ� �߻� Ƚ���� rateOfFire�� �ð� ������ ��ȯ�Ͽ� ����� �� </summary>
    float coolTime => 1 / WeaponSystem.Instance.CurrWeapon.rateOfFire;
>>>>>>> HCH_Branch
    float currTimer = 0;

    void Start()
    {
        StartCoroutine(Fire());
        weaponSystem = WeaponSystem.Instance;
<<<<<<< HEAD
        CoolTime = CoolTime;
=======
        WeaponSystem.Instance.CurrWeapon.currAmmo = WeaponSystem.Instance.CurrWeapon.maxAmmo;
>>>>>>> HCH_Branch
    }

    IEnumerator Fire()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime);
            yield return new WaitUntil(() => Input.GetButton("Fire1"));
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //������ ������ ��� (źâ�� ���� �Ѿ��� 0�� ���) Reload �Լ� ����
            WeaponSystem.Instance.CurrWeapon.currAmmo = Mathf.Clamp(--WeaponSystem.Instance.CurrWeapon.currAmmo, 0, WeaponSystem.Instance.CurrWeapon.maxAmmo);
            yield return new WaitUntil(() => AmmoCheck()); //�������� �� ������ ���
            GetFinalDamage();
            currTimer = 0.0f;
            #region ���� ���� �����
            Debug.Log(string.Format("���� �̸�: {0} \n�߻� Ÿ��: {1} \n���� ���ݷ�: {2} \n�ִ� źâ ũ��: {3}" +
                "\n���������� �ɸ��� �ӵ�: {4} \n �ʴ� ���� �ӵ�: {5} \n ġ��Ÿ Ȯ��: {6}" +
                "\n ġ��Ÿ ���: {7} \n �̵��ӵ� ������: {8}", weaponSystem.CurrWeapon.weaponName, weaponSystem.CurrWeapon.E_WeaponType,
                weaponSystem.CurrWeapon.attackDamage, weaponSystem.CurrWeapon.maxAmmo,
                weaponSystem.CurrWeapon.reloadTime, weaponSystem.CurrWeapon.rateOfFire,
                weaponSystem.CurrWeapon.criticalRate, weaponSystem.CurrWeapon.criticalMultiflier, weaponSystem.CurrWeapon.moveSpeedRate));
            #endregion
        }
    }

<<<<<<< HEAD
    /// <summary> ������ �߻� ���ð��� ��� �Լ� </summary>
=======
    Coroutine Co_Reload;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && WeaponSystem.Instance.CurrWeapon.currAmmo != WeaponSystem.Instance.CurrWeapon.maxAmmo)
        {
            if (Co_Reload != null) StopCoroutine(Co_Reload);
            Co_Reload = StartCoroutine(Reload());
        }
    }

    /// <summary> ���� �����̸� ����ϴ� �Լ� </summary>
>>>>>>> HCH_Branch
    IEnumerator CheckTime()
    {
        while(true)
        {
            if(currTimer < coolTime)
            {
                currTimer += Time.deltaTime;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                yield return new WaitForEndOfFrame();
            }
        }
    }

    bool AmmoCheck()
    {
        return WeaponSystem.Instance.CurrWeapon.currAmmo != 0 ? true : false;
    }

    IEnumerator Reload()
    {
        print("Reload!");
        yield return new WaitForSeconds(WeaponSystem.Instance.CurrWeapon.reloadTime);
        WeaponSystem.Instance.CurrWeapon.currAmmo = WeaponSystem.Instance.CurrWeapon.maxAmmo;
    }

    /// <summary> �÷��̾��� ���ݷ� ���� ���������� ����� ���� ��ȯ </summary>
    float GetFinalDamage()
    {
        float _dmg = weaponSystem.CurrWeapon.attackDamage;
        _dmg *= CriticalCheck() ? weaponSystem.CurrWeapon.criticalMultiflier / 100 : 1;
        //Debug.Log(_dmg);
        return _dmg;
    }

    bool CriticalCheck() => weaponSystem.CurrWeapon.criticalRate >= Random.Range(0, 100);
}
