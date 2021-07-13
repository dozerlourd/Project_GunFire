using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Chest : MonoBehaviour
{
    Animator anim;
    bool isOpen = false;

    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(OpenChest());
    }
    public virtual IEnumerator OpenChest()
    {
        yield return new WaitUntil(() => isOpen);
        anim.SetTrigger("IsOpen");
        yield return new WaitForSeconds(1.5f);
        InstantiateItem();
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = false;
        enabled = false;
    }

    public void OpenCheck() => isOpen = true;

    protected abstract void InstantiateItem();
}
