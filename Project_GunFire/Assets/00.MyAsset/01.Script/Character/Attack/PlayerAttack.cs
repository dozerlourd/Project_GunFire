using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator crosshairAnim;
    WeaponSystem weaponSystem;

    [SerializeField] GameObject bullet;
    [Tooltip("�ǰ� ����Ʈ"), SerializeField] GameObject fireEffect;

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

        // �ȳ��� �Ф� //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * Mathf.Infinity, Color.green);
    }

    /// <summary> ���� �����̸� ����ϴ� �Լ� </summary>
    IEnumerator CheckTime()
    {
        while (true)
        {
            if (currTimer < coolTime)
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
                HitscanAttack(_dmg);
                break;
            case Weapon.WeaponType.Projective:
                ProjectiveAttack(_dmg);
                break;
            case Weapon.WeaponType.Laser:
                LaserAttack(_dmg);
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






    void HitscanAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHitInfo))
        {
            ShowFireEffect(rayHitInfo);

            //��Ʈ��ĵ ����Ʈ �ջ�

            if (rayHitInfo.collider.tag == "T_Enemy")
            {
                rayHitInfo.collider.GetComponent<EnemyFSM>().EnemyTakeDamage(_dmg, Vector3.zero);
                ShowCrosshairEnemyHit();
                return;
            }
        }
        ShowCrosshair();
    }

    void ProjectiveAttack(float _dmg)
    {

    }

    void LaserAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHitInfo))
        {
            ShowFireEffect(rayHitInfo);

            //������ ����Ʈ �ջ�

            if (rayHitInfo.collider.tag == "T_Enemy")
            {
                rayHitInfo.collider.GetComponent<EnemyFSM>().EnemyTakeDamage(_dmg, Vector3.zero);
                ShowCrosshairEnemyHit();
                return;
            }
        }
        ShowCrosshair();
    }








    //void RayTypeScanEnemy()
    //{
    //    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //    if (Physics.Raycast(ray, out _rayHitInfo))
    //    {
            
    //    }
    //}

    void ShowFireEffect(RaycastHit _rayHit)
    {
        GameObject SFX_Clone = Instantiate(fireEffect, _rayHit.point, Quaternion.LookRotation(-_rayHit.normal));
        Debug.DrawRay(_rayHit.point, _rayHit.normal * 100, Color.red);
    }

    void ShowCrosshair()
    {
        crosshairAnim.SetTrigger("Fire");
    }

    void ShowCrosshairEnemyHit()
    {
        crosshairAnim.SetTrigger("EnemyHitFire");
    }
}
