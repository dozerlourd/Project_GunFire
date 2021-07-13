using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float dmg;
    WaitForSeconds sc = new WaitForSeconds(1.0f);
    WaitForEndOfFrame sf = new WaitForEndOfFrame();

    [SerializeField] GameObject Enemy;
    [SerializeField] GameObject particle;
    EnemyHP enemyHP;
    EnemyFSM enemyFSM;
    private void Start()
    {
        //StartCoroutine(DamagedPlayer(dmg));
        StartCoroutine(dt());
        enemyHP = Enemy.GetComponent<EnemyHP>();
        enemyFSM = Enemy.GetComponent<EnemyFSM>();

    }

    IEnumerator DamagedPlayer(float _dmg)
    {
        while(true)
        {
            yield return sf;
            enemyHP.TakeDamage = _dmg;
            if (PlayerSystem.Instance.Player) PlayerSystem.Instance.PlayerHP.TakeDamage = _dmg;
        }
    }

    IEnumerator dt()
    {
        yield return new WaitForSeconds(1.0f);
        enemyFSM.EnemyTakeDamage(enemyHP.CurrentHP + enemyHP.CurrentSheild, Vector3.zero);
        Instantiate(particle, enemyHP.transform.position, Quaternion.identity);
    }
}
