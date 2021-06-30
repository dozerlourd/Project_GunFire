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
            Debug.Log(string.Format("���� �̸�: {0} \n�߻� Ÿ��: {1} \n���� ���ݷ�: {2} \n�ִ� źâ ũ��: {3}" +
                "\n���������� �ɸ��� �ӵ�: {4} \n �ʴ� ���� �ӵ�: {5} \n ġ��Ÿ Ȯ��: {6}" +
                "\n ġ��Ÿ ���: {7} \n �̵��ӵ� ������: {8}", weaponSystem.CurrWeapon.weaponName, weaponSystem.CurrWeapon.E_WeaponType, weaponSystem.CurrWeapon.attackDamage, weaponSystem.CurrWeapon.maxAmmo,
                weaponSystem.CurrWeapon.reloadTime, weaponSystem.CurrWeapon.rateOfFire, weaponSystem.CurrWeapon.criticalRate, weaponSystem.CurrWeapon.criticalMultiflier, weaponSystem.CurrWeapon.moveSpeedRate));
            currTimer = 0.0f;
        }
    }

    /// <summary> ������ �߻� ���ð��� ��� �Լ� </summary>
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
