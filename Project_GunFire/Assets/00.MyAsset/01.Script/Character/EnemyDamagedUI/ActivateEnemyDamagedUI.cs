using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateEnemyDamagedUI : MonoBehaviour
{
    [SerializeField] GameObject DamageUI;

    public GameObject CreateDamageUI()
    {
        return Instantiate(DamageUI);
    }

    public GameObject CreateDamageUI(Transform _parent)
    {
        return Instantiate(DamageUI, _parent);
    }

    public GameObject CreateDamageUI(Vector3 _vec, Quaternion _qt)
    {
        return Instantiate(DamageUI, _vec, _qt);
    }

    public GameObject CreateDamageUI(Vector3 _vec, Quaternion _qt, Transform _parent)
    {
        return Instantiate(DamageUI, _vec, _qt, _parent);
    }
}
