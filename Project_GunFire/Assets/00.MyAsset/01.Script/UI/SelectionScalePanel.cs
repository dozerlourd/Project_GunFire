using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectionScalePanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    bool isCheck = false;
    Coroutine Co_CheckSelection;

    void OnEnable()
    {
        isCheck = false;
        transform.localScale = Vector3.one;
        if (Co_CheckSelection != null) StopCoroutine(Co_CheckSelection);
        Co_CheckSelection = StartCoroutine(CheckSelection());
    }

    IEnumerator CheckSelection()
    {
        while(true)
        {
            yield return new WaitForSecondsRealtime(0.02f);
            transform.localScale = isCheck ? new Vector3(1.1f, 1.1f, 1) : Vector3.one;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isCheck = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isCheck = false;
    }
}
