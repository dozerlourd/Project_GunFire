using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    WeaponSystem weaponSystem;

    public float CoolTime
    {
        get => coolTime; set => coolTime = 1 / WeaponSystem.Instance.CurrWeapon.rateOfFire;
    }
    [SerializeField] float coolTime = 0.35f;
    float currTimer = 0;
    void Start()
    {
        StartCoroutine(FireCheck());
        weaponSystem = WeaponSystem.Instance;
        CoolTime = CoolTime;
    }

    IEnumerator FireCheck()
    {
        StartCoroutine(CheckTime());
        while (true)
        {
            yield return new WaitUntil(() => currTimer >= coolTime);
            yield return new WaitUntil(() => Input.GetButton("Fire1"));
            //Instantiate(bullet, transform.position, Quaternion.identity);
            Debug.Log(string.Format("무기 이름: {0} \n발사 타입: {1} \n무기 공격력: {2} \n최대 탄창 크기: {3}" +
                "\n재장전까지 걸리는 속도: {4} \n 초당 공격 속도: {5} \n 치명타 확률: {6}" +
                "\n 치명타 배수: {7} \n 이동속도 증감률: {8}", weaponSystem.CurrWeapon.weaponName, weaponSystem.CurrWeapon.E_WeaponType, weaponSystem.CurrWeapon.attackDamage, weaponSystem.CurrWeapon.maxAmmo,
                weaponSystem.CurrWeapon.reloadTime, weaponSystem.CurrWeapon.rateOfFire, weaponSystem.CurrWeapon.criticalRate, weaponSystem.CurrWeapon.criticalMultiflier, weaponSystem.CurrWeapon.moveSpeedRate));
            currTimer = 0.0f;
        }
    }

    /// <summary> 무기의 발사 대기시간을 재는 함수 </summary>
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
}
