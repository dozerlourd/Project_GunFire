using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Gun
{
    

    [Tooltip("���� �̸�"), SerializeField] public string name;
    public enum GunType
    {
        Hitscan = 0,
        Projective = 1
    }
    [Tooltip("�߻� Ÿ�� (��Ʈ��ĵ, ����ü)"), SerializeField] public GunType EGunType;
    [Tooltip("���� ���ݷ�"), SerializeField] public float attackDamage;
    [Tooltip("�ִ� źâ ũ��"), SerializeField] public float maxAmmo;
    [Tooltip("���������� �ɸ��� �ӵ�"), SerializeField] public float reloadTime;
    [Tooltip("�ʴ� ���� �ӵ� (0.01 ~ 20.0)"), Range(0.01f, 20.0f), SerializeField] public float rateOfFire;
    [Tooltip("ġ��Ÿ Ȯ�� (0 ~ 100)"), Range(0.01f, 100.0f), SerializeField] public float criticalRate;
    [Tooltip("ġ��Ÿ ��� (100.0f ~ 500.0f)"), Range(100.0f, 500.0f), SerializeField] public float criticalMultiflier;
    [Tooltip("�̵��ӵ� ������ (-100.0f ~ 100.0f"), Range(-100.0f, 100.0f), SerializeField] public float moveSpeedRate;
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
