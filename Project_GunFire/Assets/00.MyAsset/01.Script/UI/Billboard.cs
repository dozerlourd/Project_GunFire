using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] bool isUI = false;
    float dir;

    void Update()
    {
        dir = isUI ? 1 : -1;
        transform.forward = Camera.main.transform.forward * dir;
    }
}
