using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ChainMonster : MonoBehaviour
{
    [SerializeField] GameObject disruptiveForce;
    [SerializeField] float deactiveTime, radius = 3.5f, effectDuration = 3.5f;

    public float ChainReleasedTime
    {
        get
        {
            //// 만약 각성 1이면 리턴 chainReleasedTime + 2.
            //if() return chainReleasedTime + 2;
            //// 그렇지 않고, 각성 2이면 리턴 chainReleasedTime + 4.
            //else if() return chainReleasedTime + 4;
            //// 그렇지 않고, 각성 3이면 리턴 chainReleasedTime + 6.
            //else if() return chainReleasedTime + 4;
            //// 그렇지 않고, 각성 없으면 리턴 chainReleasedTime.
            //else
            return chainReleasedTime;
        }
    }
    [SerializeField, Tooltip("체인 풀리는 시간")] float chainReleasedTime;

    private void Start()
    {
        Invoke("Deactivate",deactiveTime);
    }

    void Deactivate() { Destroy(gameObject); }

    private void OnTriggerEnter(Collider col)
    {
        Collider[] enemyCols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("L_Enemy"));
        Debug.Log(enemyCols.Length);

        foreach (Collider enemy in enemyCols)
        {
            EnemyFSM enemyFSM = enemy.GetComponent<EnemyFSM>();
            enemyFSM.IsChained = true;
            Debug.Log("Chain!");
            enemyFSM.StartCoroutine(enemyFSM.ChainDeactivate(ChainReleasedTime));
        }
        GameObject effectClone = Instantiate(disruptiveForce, transform.position, Quaternion.identity);
        Destroy(effectClone, effectDuration);
        Deactivate();
    }
}
 