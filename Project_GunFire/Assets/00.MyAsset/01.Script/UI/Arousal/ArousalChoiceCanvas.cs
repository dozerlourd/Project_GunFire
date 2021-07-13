using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArousalChoiceCanvas : MonoBehaviour
{
    Coroutine co;
    [SerializeField] List<GameObject> ButtonList = new List<GameObject>();
    int arousalNum = 0;

    private void OnEnable()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;

        for (int i = 0; i < ButtonList.Count; i++)
        {
            ButtonList[i].SetActive(false);
        }
        for (int i = 0; i < 3; i++)
        {
            while (true)
            {
                int temp = Random.Range(0, ButtonList.Count);
                if (ButtonList[temp].activeSelf == false)
                {
                    ButtonList[temp].SetActive(true);
                    break;
                }
            }
        }

        co = StartCoroutine(CheckArousal());
    }

    private void OnDisable()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Time.timeScale = 1;
    }

    IEnumerator CheckArousal()
    {
        yield return new WaitUntil(() => arousalNum != 0);
        //StopCoroutine(co);
        switch (arousalNum)
        {
            case 1:
                PlayerArousal.Instance.UpgradeBackpackExpension();
                arousalNum = 0;
                gameObject.SetActive(false);
                break;

            case 2:
                PlayerArousal.Instance.UpgradeEnchantmentLast();
                arousalNum = 0;
                gameObject.SetActive(false);
                break;

            case 3:
                PlayerArousal.Instance.UpgradePowerSource();
                arousalNum = 0;
                gameObject.SetActive(false);
                break;

            case 4:
                PlayerArousal.Instance.UpgradeToxicSmoke();
                arousalNum = 0;
                gameObject.SetActive(false);
                break;

            default:

                break;
        }
    }

    /// <summary>
    /// 각성 업그레이드 함수
    /// </summary>
    /// <param name="num"> 1번: 가방 확장(수류탄 가방 확장), 2번: 극독 안개(수류탄 데미지 증가) 3번: 강력한 속박(속박 지속시간 증가) 4번: 힘의 원천(에너지 오브 사용량 증가)</param>
    public void OnClickArousalButton(int num)
    {
        arousalNum = num;
    }
}
