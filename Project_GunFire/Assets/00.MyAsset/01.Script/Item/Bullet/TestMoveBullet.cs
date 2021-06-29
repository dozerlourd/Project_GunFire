using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMoveBullet : MonoBehaviour
{
    void Update()
    {
        transform.position += transform.forward * 10 * Time.deltaTime;
    }
}
