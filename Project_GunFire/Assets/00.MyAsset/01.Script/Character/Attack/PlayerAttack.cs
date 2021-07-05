using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator crosshairAnim;
    WeaponSystem weaponSystem;

    [SerializeField] GameObject bullet;
    [Tooltip("피격 이펙트"), SerializeField] GameObject fireEffect;

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
    /// 5. 
    /// 6. 애니메이션 및 최종 데미지로 Enemy에게 피해를 주는 로직 구현 예정
    /// 7. 타이머에 누적된 값을 초기화시켜준다.
    IEnumerator Fire()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime); // 1.
            yield return new WaitUntil(() => AmmoCheck()); // 2. 재장전이 될 때까지 대기
            yield return new WaitUntil(() => Input.GetButton("Fire1")); // 3.
            weaponSystem.CurrWeapon.currAmmo = Mathf.Clamp(--weaponSystem.CurrWeapon.currAmmo, 0, weaponSystem.CurrWeapon.maxAmmo); // 4.
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //미장전 상태일 경우 (탄창에 현재 총알이 0일 경우) Reload 함수 실행
            Attack(GetFinalDamage()); // 5. 최종 데미지 계산 및 적에게 피해 적용 (Enemy에게 피해를 주는 로직은 구현 예정에 있음)
            // 6. 애니메이션 적용 (프로토타입 일정 후 구현 예정)
            currTimer = 0.0f; //7.
            #region 무기 정보 디버깅
            Debug.Log(string.Format("무기 이름: {0} \n발사 타입: {1} \n무기 공격력: {2} \n최대 탄창 크기: {3}" +
                "\n재장전까지 걸리는 속도: {4} \n 초당 공격 속도: {5} \n 치명타 확률: {6}" +
                "\n 치명타 배수: {7} \n 이동속도 증감률: {8}", weaponSystem.CurrWeapon.weaponName, weaponSystem.CurrWeapon.E_WeaponType,
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
        _dmg *= CriticalCheck() ? weaponSystem.CurrWeapon.criticalMultiflier / 100 : 1;
        //Debug.Log(_dmg);
        return _dmg;
    }

    /// <summary> Random 함수를 이용하여 치명타 적용의 여부를 판단하는 함수 </summary>
    /// <returns> 치명타율의 적용 여부 </returns>
    bool CriticalCheck() => weaponSystem.CurrWeapon.criticalRate >= Random.Range(0, 100);






    void HitscanAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHitInfo))
        {
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

    }

    void LaserAttack(float _dmg)
    {
        RaycastHit rayHitInfo;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out rayHitInfo))
        {
            ShowFireEffect(rayHitInfo);

            //레이저 이펙트 뿜뿜

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
