using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun
{
    

    [Tooltip("무기 이름"), SerializeField] public string name;
    public enum GunType
    {
        Hitscan = 0,
        Projective = 1
    }
    [Tooltip("발사 타입 (히트스캔, 투사체)"), SerializeField] public GunType EGunType;
    [Tooltip("무기 공격력"), SerializeField] public float attackDamage;
    [Tooltip("최대 탄창 크기"), SerializeField] public float maxAmmo;
    [Tooltip("재장전까지 걸리는 속도"), SerializeField] public float reloadTime;
    [Tooltip("초당 공격 속도 (0.01 ~ 20.0)"), Range(0.01f, 20.0f), SerializeField] public float rateOfFire;
    [Tooltip("치명타 확률 (0 ~ 100)"), Range(0.01f, 100.0f), SerializeField] public float criticalRate;
    [Tooltip("치명타 배수 (100.0f ~ 500.0f)"), Range(100.0f, 500.0f), SerializeField] public float criticalMultiflier;
    [Tooltip("이동속도 증감률 (-100.0f ~ 100.0f"), Range(-100.0f, 100.0f), SerializeField] public float moveSpeedRate;
}

public class GunSystem : MonoBehaviour
{
    public Gun[] Guns => guns;
    [SerializeField] Gun[] guns;

    public Gun CurrGun
    {
        get => currGun; set
        {
            currGun = value;
        }
    }
    private Gun currGun;
}
