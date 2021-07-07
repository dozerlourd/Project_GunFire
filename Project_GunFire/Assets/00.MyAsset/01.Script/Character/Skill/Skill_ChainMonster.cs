using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ChainMonster : MonoBehaviour
{
    [SerializeField] float deactiveTime;
    SphereCollider sphereCollider;

    private void Start()
    {
        sphereCollider = GetComponent<SphereCollider>();
        Invoke("Deactivate",deactiveTime);
    }

    void Deactivate()
    {
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider col)
    {
        Collider[] enemyCols = Physics.OverlapSphere(col.transform.position, 15.0f, LayerMask.GetMask("L_Enemy"));
        Debug.Log(enemyCols.Length);

        foreach (Collider enemy in enemyCols)
        {
            EnemyFSM enemyFSM = enemy.GetComponent<EnemyFSM>();
            enemyFSM.IsChained = true;
            Debug.Log("Chain!");
            enemyFSM.StartCoroutine(enemyFSM.ChainDeactivate());
        }
        gameObject.SetActive(false);
    }
}
 