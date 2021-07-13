using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraVFX_PoisonPad : MonoBehaviour
{
    WaitForSeconds cycle;

    [SerializeField] float radius = 3.5f;
    float Cooltime = 1.0f;

    public float Damage => _ = PlayerSystem.Instance.PlayerArousal.ToxicSmoke == 3 ?
        PlayerSystem.Instance.PlayerArousal.ToxicDamage * damageCount++ :
        PlayerSystem.Instance.PlayerArousal.ToxicDamage;
    float damageCount = 1;

    private void Start()
    {
        cycle = new WaitForSeconds(Cooltime);
        StartCoroutine(PoisonPadDamage());
    }

    IEnumerator PoisonPadDamage()
    {
        while(true)
        {
            Collider[] enemyCols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("L_Enemy"));

            foreach (Collider enemy in enemyCols)
            {
                EnemyFSM enemyFSM = enemy.GetComponent<EnemyFSM>();
                enemyFSM.EnemyTakeDamage(Damage, Vector3.zero);
            }
            yield return cycle;
        }
    }
}
