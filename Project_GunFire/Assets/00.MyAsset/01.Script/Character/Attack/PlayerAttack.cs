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
    /// <summary> 무기의 초당 발사 횟수인 rateOfFire을 시간 단위로 변환하여 계산한 값 </summary>
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
            //if (!AmmoCheck()) yield return StartCoroutine(Reload()); //미장전 상태일 경우 (탄창에 현재 총알이 0일 경우) Reload 함수 실행
            WeaponSystem.Instance.CurrWeapon.currAmmo = Mathf.Clamp(--WeaponSystem.Instance.CurrWeapon.currAmmo, 0, WeaponSystem.Instance.CurrWeapon.maxAmmo);
            yield return new WaitUntil(() => AmmoCheck()); //재장전이 될 때까지 대기
            GetFinalDamage();
            currTimer = 0.0f;
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

<<<<<<< HEAD
    /// <summary> 무기의 발사 대기시간을 재는 함수 </summary>
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

    /// <summary> 공격 딜레이를 계산하는 함수 </summary>
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

    /// <summary> 플레이어의 공격력 값을 최종적으로 계산한 값을 반환 </summary>
    float GetFinalDamage()
    {
        float _dmg = weaponSystem.CurrWeapon.attackDamage;
        _dmg *= CriticalCheck() ? weaponSystem.CurrWeapon.criticalMultiflier / 100 : 1;
        //Debug.Log(_dmg);
        return _dmg;
    }

    bool CriticalCheck() => weaponSystem.CurrWeapon.criticalRate >= Random.Range(0, 100);
}
