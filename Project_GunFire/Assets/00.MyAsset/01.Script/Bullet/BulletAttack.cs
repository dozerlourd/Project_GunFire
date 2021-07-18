using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletAttack : MonoBehaviour
{
    public float SetBulletDamage { set => bulletDamage = value; }
    float bulletDamage;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "T_Enemy")
        {
            col.gameObject.GetComponent<EnemyFSM>().EnemyTakeDamage(bulletDamage, Vector3.zero);
        }
        Destroy(this.gameObject);
    }
}
