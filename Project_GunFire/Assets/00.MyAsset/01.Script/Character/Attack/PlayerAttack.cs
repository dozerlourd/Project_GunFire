using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    WeaponSystem weaponSystem;

    [SerializeField] GameObject bullet;

    /// <summary> ������ �ʴ� �߻� Ƚ���� rateOfFire�� �� ������ ��ȯ�Ͽ� ����� �� </summary>
    float coolTime => 1 / WeaponSystem.Instance.CurrWeapon.rateOfFire;
    float currTimer = 0;

    void Start()
    {
        StartCoroutine(Fire());
        weaponSystem = WeaponSystem.Instance;
        weaponSystem.CurrWeapon.currAmmo = weaponSystem.CurrWeapon.maxAmmo;
    }

    /// <summary> �÷��̾��� ���� �Լ� </summary>
    /// 1. �߻� ��� �ð� üũ -> ��� �ð��� ������ 2������
    /// 2. źâ Ȯ�� ��, źâ�� �Ѿ��� ���� ���, �������� �� ������ ���
    /// 3. �Է� üũ -> ���� �Է��� ������ ���, 4������
    /// 4. �����ִ� �Ѿ��� ���� üũ -> �������� ���� ���, ���������� ���
    /// 5. 
    /// 6. �ִϸ��̼� �� ���� �������� Enemy���� ���ظ� �ִ� ���� ���� ����
    /// 7. Ÿ�̸ӿ� ������ ���� �ʱ�ȭ�����ش�.
    IEnumerator Fire()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime); // 1.
            yield return new WaitUntil(() => AmmoCheck()); // 2. �������� �� ������ ���
            yield return new WaitUntil(() => Input.GetButton("Fire1")); // 3.
            weaponSystem.CurrWeapon.currAmmo = Mathf.Clamp(--weaponSystem.CurrWeapon.currAmmo, 0, weaponSystem.CurrWeapon.maxAmmo); // 4.
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //������ ������ ��� (źâ�� ���� �Ѿ��� 0�� ���) Reload �Լ� ����
            Attack(GetFinalDamage()); // 5. ���� ������ ��� �� ������ ���� ���� (Enemy���� ���ظ� �ִ� ������ ���� ������ ����)
            // 6. �ִϸ��̼� ���� (������Ÿ�� ���� �� ���� ����)
            currTimer = 0.0f; //7.
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

    Coroutine Co_Reload;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && weaponSystem.CurrWeapon.currAmmo != weaponSystem.CurrWeapon.maxAmmo)
        {
            if (Co_Reload != null) StopCoroutine(Co_Reload);
            Co_Reload = StartCoroutine(Reload());
        }
    }

    /// <summary> ���� �����̸� ����ϴ� �Լ� </summary>
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

    /// <summary> ���� ������ źâ�� �����ִ� �Ѿ��� ������ üũ�ϴ� �Լ� </summary>
    /// <returns>  ���� ������ źâ�� �����ִ� �Ѿ��� ������ bool ������ ��ȯ </returns>
    bool AmmoCheck()
    {
        if (Input.GetButtonDown("Fire1") && weaponSystem.CurrWeapon.currAmmo == 0)
        {
            //SoundManager.Instance.EmptyAmmoSound(); // ����ִ� źâ ����
        }
        return weaponSystem.CurrWeapon.currAmmo != 0 ? true : false;
    }

    IEnumerator Reload()
    {
        print("Reload!");
        yield return new WaitForSeconds(weaponSystem.CurrWeapon.reloadTime);
        weaponSystem.CurrWeapon.currAmmo = weaponSystem.CurrWeapon.maxAmmo;
    }

    void Attack(float _dmg)
    {
        switch(weaponSystem.CurrWeapon.E_WeaponType)
        {
            case Weapon.WeaponType.Hitscan:
                HitscanAttack();
                break;
            case Weapon.WeaponType.Projective:
                ProjectiveAttack();
                break;
            case Weapon.WeaponType.Laser:
                LaserAttack();
                break;
        }

    }

    /// <summary> �÷��̾��� ���ݷ� ���� ���������� ����� ���� ��ȯ (�׷����� -> ���������� = ���ⵥ���� * ġ��Ÿ ? ġ��Ÿ ���� : 100%) </summary>
    /// <returns>  ������ ���� ���� ������ �� </returns>
    float GetFinalDamage()
    {
        float _dmg = weaponSystem.CurrWeapon.attackDamage;
        _dmg *= CriticalCheck() ? weaponSystem.CurrWeapon.criticalMultiflier / 100 : 1;
        //Debug.Log(_dmg);
        return _dmg;
    }

    /// <summary> Random �Լ��� �̿��Ͽ� ġ��Ÿ ������ ���θ� �Ǵ��ϴ� �Լ� </summary>
    /// <returns> ġ��Ÿ���� ���� ���� </returns>
    bool CriticalCheck() => weaponSystem.CurrWeapon.criticalRate >= Random.Range(0, 100);

    void HitscanAttack()
    {

    }

    void ProjectiveAttack()
    {

    }

    void LaserAttack()
    {

    }
}
