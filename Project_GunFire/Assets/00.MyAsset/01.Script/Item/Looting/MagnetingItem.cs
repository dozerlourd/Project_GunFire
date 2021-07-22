using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetingItem : MonoBehaviour
{
    [SerializeField] float detectiveDist = 5.0f;
    bool changeTrigger = false;

    void Start()
    {
        StartCoroutine(MagnetToPlayer());
    }

    IEnumerator MagnetToPlayer()
    {
        Transform player;
        player = PlayerSystem.Instance.Player.transform;
        while (true)
        {
            float dist = Vector3.Distance(player.position, transform.position);

            if (dist <= detectiveDist)
            {
                if(!changeTrigger) { ChangeTrigger(); changeTrigger = true; }
                transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, -0.5f, 0), 0.05f);
            }
            yield return new WaitForEndOfFrame();
        }
    }

    void ChangeTrigger()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Collider>().isTrigger = true;
    }

    

    protected virtual void ItemEffect() { }
}
