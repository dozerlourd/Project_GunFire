using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator crosshairAnim;
    WeaponSystem weaponSystem;

    [SerializeField] Transform firePos;

    /// <summary> 무기의 초당 발사 횟수인 rateOfFire을 초 단위로 변환하여 계산한 값 </summary>
    float coolTime => 1 / WeaponSystem.Instance.CurrWeapon.rateOfFire;
    float currTimer = 0;

    void Start()
    {
        StartCoroutine(Fire());
        weaponSystem = WeaponSystem.Instance;
        weaponSystem.CurrWeapon.currAmmo = weaponSystem.CurrWeapon.maxAmmo;
    }

    /// <summary> 플레이어의 공격 함수 </summary>
    /// 1. 발사 대기 시간 체크 -> 대기 시간이 지나면 2번으로
    /// 2. 탄창 확인 후, 탄창에 총알이 없을 경우, 재장전이 될 때까지 대기
    /// 3. 입력 체크 -> 공격 입력이 들어왔을 경우, 4번으로
    /// 4. 남아있는 총알의 개수 체크 -> 남아있지 않을 경우, 재장전까지 대기
    /// 5. 최종 데미지 계산 및 적에게 피해 적용 -> 함수를 통하여 무기 타입에 따라 다르게 구현
    /// 6. 타이머에 누적된 값을 초기화시켜준다.
    IEnumerator Fire()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime); // 1.
            yield return new WaitUntil(() => AmmoCheck()); // 2. 재장전이 될 때까지 대기
            yield return new WaitUntil(() => Input.GetButton("Fire1")); // 3.
            if (weaponSystem.CurrWeapon.E_WeaponType == Weapon.WeaponType.Laser) SoundManager.Instance.WeaponSound(1.0f);
            else SoundManager.Instance.WeaponSound(0.3f);
            weaponSystem.CurrWeapon.currAmmo = Mathf.Clamp(--weaponSystem.CurrWeapon.currAmmo, 0, weaponSystem.CurrWeapon.maxAmmo); // 4.
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //미장전 상태일 경우 (탄창에 현재 총알이 0일 경우) Reload 함수 실행
            Attack(GetFinalDamage()); // 5. 최종 데미지 계산 및 적에게 피해 적용 (Enemy에게 피해를 주는 로직은 구현 예정에 있음)
            currTimer = 0.0f; //6. 타이머에 누적된 값을 초기화시켜준다.
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

        // 안나와 ㅠㅠ //Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * Mathf.Infinity, Color.green);
    }

    /// <summary> 공격 딜레이를 계산하는 함수 </summary>
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

    /// <summary> 현재 무기의 탄창에 남아있는 총알의 유무를 체크하는 함수 </summary>
    /// <returns>  현재 무기의 탄창에 남아있는 총알의 유무를 bool 값으로 반환 </returns>
    bool AmmoCheck()
    {
        if (Input.GetButtonDown("Fire1") && weaponSystem.CurrWeapon.currAmmo == 0)
        {
            //SoundManager.Instance.EmptyAmmoSound(); // 비어있는 탄창 사운드
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

    /// <summary> 플레이어의 공격력 값을 최종적으로 계산한 값을 반환 (그래봤자 -> 최종데미지 = 무기데미지 * 치명타 ? 치명타 배율 : 100%) </summary>
    /// <returns>  적에게 입힐 최종 데미지 값 </returns>
    float GetFinalDamage()
    {
        float _dmg = weaponSystem.CurrWeapon.attackDamage;
        _dmg *= CriticalCheck() ? (weaponSystem.CurrWeapon.criticalMultiflier / 100) : 1;
        return _dmg;
    }

    /// <summary> Random 함수를 이용하여 치명타 적용의 여부를 판단하는 함수 </summary>
    /// <returns> 치명타율의 적용 여부 </returns>
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

            //히트스캔 이펙트 뿜뿜

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

            //레이저 이펙트 뿜뿜
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
