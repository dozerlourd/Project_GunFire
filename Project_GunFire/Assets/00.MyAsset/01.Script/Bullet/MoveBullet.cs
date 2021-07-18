using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour
{
    [SerializeField] float bulltSpeed = 20.0f;
    void Update()
    {
        transform.position += transform.forward * bulltSpeed * Time.deltaTime;
    }
}
