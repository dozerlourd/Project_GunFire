using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator crosshairAnim;
    WeaponSystem weaponSystem;

    [SerializeField] Transform firePos;

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
    /// 5. ���� ������ ��� �� ������ ���� ���� -> �Լ��� ���Ͽ� ���� Ÿ�Կ� ���� �ٸ��� ����
    /// 6. Ÿ�̸ӿ� ������ ���� �ʱ�ȭ�����ش�.
    IEnumerator Fire()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime); // 1.
            yield return new WaitUntil(() => AmmoCheck()); // 2. �������� �� ������ ���
            yield return new WaitUntil(() => Input.GetButton("Fire1")); // 3.
            if (weaponSystem.CurrWeapon.E_WeaponType == Weapon.WeaponType.Laser) SoundManager.Instance.WeaponSound(1.0f);
            else SoundManager.Instance.WeaponSound(0.3f);
            weaponSystem.CurrWeapon.currAmmo = Mathf.Clamp(--weaponSystem.CurrWeapon.currAmmo, 0, weaponSystem.CurrWeapon.maxAmmo); // 4.
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //������ ������ ��� (źâ�� ���� �Ѿ��� 0�� ���) Reload �Լ� ����
            Attack(GetFinalDamage()); // 5. ���� ������ ��� �� ������ ���� ���� (Enemy���� ���ظ� �ִ� ������ ���� ������ ����)
            currTimer = 0.0f; //6. Ÿ�̸ӿ� ������ ���� �ʱ�ȭ�����ش�.
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
            if (Co_Reload != null) StopCoroutine(Co_Reload);
            Co_Reload = StartCoroutine(Reload());
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
        _dmg *= CriticalCheck() ? (weaponSystem.CurrWeapon.criticalMultiflier / 100) : 1;
        return _dmg;
    }

    /// <summary> Random �Լ��� �̿��Ͽ� ġ��Ÿ ������ ���θ� �Ǵ��ϴ� �Լ� </summary>
    /// <returns> ġ��Ÿ���� ���� ���� </returns>
    bool CriticalCheck() => weaponSystem.CurrWeapon.criticalRate >= Random.Range(0, 100);






    void HitscanAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition + new Vector3(
                  Random.Range(0, weaponSystem.CurrWeapon.fireSpreadRate),
                  Random.Range(0, weaponSystem.CurrWeapon.fireSpreadRate), 0));
        
        if (Physics.Raycast(ray, out rayHitInfo, 1 << 0 | 1 << 7 | 1 << 12))
        {
            //print(rayHitInfo.collider.name);
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
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Vector3 _dir;

        GameObject _bulletClone = Instantiate(weaponSystem.CurrWeapon.projectiveBullet);
        if (Physics.Raycast(ray, out rayHitInfo))
        {
            _dir = rayHitInfo.transform.position - firePos.position;
        }
            
            _bulletClone.transform.position = firePos.position;
            _bulletClone.transform.forward = firePos.forward;
        
        _bulletClone.GetComponent<BulletAttack>().SetBulletDamage = _dmg;
    }

    void LaserAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHitInfo, 1 << 0 | 1 << 7 | 1 << 12))
        {
            //print(rayHitInfo.collider.gameObject.layer);
            if (rayHitInfo.collider.gameObject.layer == 0 ||
                rayHitInfo.collider.gameObject.layer == 7 ||
                rayHitInfo.collider.gameObject.layer == 12)
            {
                ShowFireEffect(rayHitInfo);
            }

            //������ ����Ʈ �ջ�
            DrawLaserLine(rayHitInfo.transform.position);

            if (rayHitInfo.collider.tag == "T_Enemy")
            {
                rayHitInfo.collider.GetComponent<EnemyFSM>().EnemyTakeDamage(_dmg, Vector3.zero);
                ShowCrosshairEnemyHit();
                return;
            }
        }
        ShowCrosshair();
    }

    void DrawLaserLine(Vector3 hitVec)
    {

    }






    

    void ShowFireEffect(RaycastHit _rayHit)
    {
        GameObject SFX_Clone = Instantiate(weaponSystem.CurrWeapon?.hitEffect, _rayHit.point, Quaternion.LookRotation(_rayHit.normal));
        Destroy(SFX_Clone, 1.5f);
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
