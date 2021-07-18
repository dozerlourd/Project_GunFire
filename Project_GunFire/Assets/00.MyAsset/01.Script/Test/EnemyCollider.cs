using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollider : MonoBehaviour
{
    bool enemySpawn = false;

    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "T_Player" && !enemySpawn)
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
}
