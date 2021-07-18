using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    [SerializeField] AudioClip clearSound;

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "T_Player")
        {
            SoundManager.Instance.PlayOneShot(clearSound, 0.5f);
        }
    }
}
