using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] float dmg;
    WaitForSeconds sc = new WaitForSeconds(1.0f);
    private void Start()
    {
        StartCoroutine(DamagedPlayer(dmg));
    }

    IEnumerator DamagedPlayer(float _dmg)
    {
        while(true)
        {
            yield return sc;
            PlayerSystem.Instance.PlayerHP.TakeDamage = _dmg;
        }
    }
}
