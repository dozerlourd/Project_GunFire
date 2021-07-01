using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float dmg;
    WaitForSeconds sc = new WaitForSeconds(1.0f);
    WaitForEndOfFrame sf = new WaitForEndOfFrame();

    [SerializeField] GameObject Enemy;
    EnemyHP enemyHP;
    private void Start()
    {
        StartCoroutine(DamagedPlayer(dmg));
         enemyHP = Enemy.GetComponent<EnemyHP>();
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
}
