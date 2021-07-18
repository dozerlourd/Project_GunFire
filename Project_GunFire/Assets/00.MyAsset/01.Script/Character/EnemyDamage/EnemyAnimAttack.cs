using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimAttack : MonoBehaviour
{
    EnemyFSM enemyFSM;

    private void Start()
    {
        enemyFSM = transform.parent.GetComponent<EnemyFSM>();
    }
    public void EnemyDamage()
    {
        if (enemyFSM.GetDistance() > enemyFSM.attackRange * 1.35f) return;

        enemyFSM.GetDamageToPlayer();
        print("damage");
    }
}
