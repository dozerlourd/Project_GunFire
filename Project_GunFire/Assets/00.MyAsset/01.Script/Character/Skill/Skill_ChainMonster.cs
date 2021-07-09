using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_ChainMonster : MonoBehaviour
{
    [SerializeField] GameObject disruptiveForce;
    [SerializeField] float deactiveTime, radius = 3.5f, effectDuration = 3.5f;

    PlayerArousal playerArousal;

    private void Start()
    {
        Invoke("Deactivate",deactiveTime);
        playerArousal = PlayerSystem.Instance.PlayerArousal;
    }

    void Deactivate() { Destroy(gameObject); }

    private void OnTriggerEnter(Collider col)
    {
        Collider[] enemyCols = Physics.OverlapSphere(transform.position, radius, LayerMask.GetMask("L_Enemy"));

        foreach (Collider enemy in enemyCols)
        {
            EnemyFSM enemyFSM = enemy.GetComponent<EnemyFSM>();
            enemyFSM.IsChained = true;
            Debug.Log("Chain!");
            enemyFSM.StartCoroutine(enemyFSM.ChainDeactivate(playerArousal.ChainReleasedTime));
        }

        GameObject effectClone = Instantiate(disruptiveForce, transform.position, Quaternion.identity);
        Destroy(effectClone, effectDuration);
        Deactivate();
    }
}
