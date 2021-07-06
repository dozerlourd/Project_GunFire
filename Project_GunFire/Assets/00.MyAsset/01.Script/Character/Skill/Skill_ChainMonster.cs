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
        if (col.gameObject.tag != "T_Enemy") return;

        sphereCollider.radius = 13.5f;
        EnemyFSM enemyFSM = col.GetComponent<EnemyFSM>();
        enemyFSM.IsChained = true;
        Debug.Log("Chain!");
        enemyFSM.StartCoroutine(enemyFSM.ChainDeactivate());
        gameObject.SetActive(false);
    }
}
 