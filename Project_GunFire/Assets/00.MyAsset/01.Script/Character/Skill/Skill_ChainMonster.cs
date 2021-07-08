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
            //// ���� ���� 1�̸� ���� chainReleasedTime + 2.
            //if() return chainReleasedTime + 2;
            //// �׷��� �ʰ�, ���� 2�̸� ���� chainReleasedTime + 4.
            //else if() return chainReleasedTime + 4;
            //// �׷��� �ʰ�, ���� 3�̸� ���� chainReleasedTime + 6.
            //else if() return chainReleasedTime + 4;
            //// �׷��� �ʰ�, ���� ������ ���� chainReleasedTime.
            //else
            return chainReleasedTime;
        }
    }
    [SerializeField, Tooltip("ü�� Ǯ���� �ð�")] float chainReleasedTime;

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
 