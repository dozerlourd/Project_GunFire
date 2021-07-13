using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Co_Effect());
    }

    IEnumerator Co_Effect()
    {
        yield return new WaitForSeconds(0.5f);

        Destroy(gameObject);
    }
}
