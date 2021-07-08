using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject ChainSphere, Granage;
    [SerializeField] float SkillMoveSpeed_E, SkillMoveSpeed_Q;


    public int MaxCount_Q
    {
        get
        {
            //// 만약 각성 1이면 리턴 maxCount_Q + 4.
            //if () return maxCount_Q;
            //// 그렇지 않고, 각성 2이면 리턴 maxCount_Q + 8.
            //else if () return maxCount_Q;
            //// 그렇지 않고, 각성 3이면 리턴 maxCount_Q + 12.
            //else if () return maxCount_Q;
            //// 그렇지 않고, 각성 2이면 리턴 maxCount_Q.
            //else
            return maxCount_Q;
        }
    }
    [SerializeField, Tooltip("수류탄 최대 소지량")] int maxCount_Q;
    public int SetMaxCount_Q(int value) => maxCount_Q = value;

    [SerializeField] float coolTime_E;

    public int SkillCount_Q
    {
        get => skillCount_Q;
    }
    int skillCount_Q = 0;
    float currTimer_E = 0f;
    public void SetSkillCount_Q(int value) => skillCount_Q = value;

    WaitUntil SkillCountCheck_Q, coolTimeCheck_E, InputCheck_Q, InputCheck_E;

    private void Start()
    {
        StartCoroutine(CheckTime());
        StartCoroutine(Co_SkillType_Q());
        StartCoroutine(Co_SkillType_E());

        SkillCountCheck_Q = new WaitUntil(() => skillCount_Q > 0);
        coolTimeCheck_E = new WaitUntil(() => currTimer_E >= coolTime_E);
        InputCheck_Q = new WaitUntil(() => Input.GetKeyDown(KeyCode.Q));
        InputCheck_E = new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        skillCount_Q = MaxCount_Q;
        currTimer_E = coolTime_E;
    }

    IEnumerator CheckTime()
    {
        while (true)
        {
            currTimer_E += currTimer_E < coolTime_E ? Time.deltaTime : 0;
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Co_SkillType_Q()
    {
        while (true)
        {
            if (skillCount_Q <= 0) yield return null;
            yield return SkillCountCheck_Q;
            yield return InputCheck_Q;

            SetSkillCount_Q(SkillCount_Q - 1);
            Debug.Log("Q");
            GameObject SkillClone = Instantiate(Granage, transform.position, Quaternion.identity);
            SkillClone.SetActive(true);
            SkillClone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * SkillMoveSpeed_Q, ForceMode.Impulse);
        }
    }

    IEnumerator Co_SkillType_E()
    {
        while (true)
        {
            yield return coolTimeCheck_E;
            yield return InputCheck_E;

            Debug.Log("E");
            GameObject SkillClone = Instantiate(ChainSphere, transform.position, Quaternion.identity);
            SkillClone.SetActive(true);
            SkillClone.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * SkillMoveSpeed_E, ForceMode.Impulse);
            InitTime(ref currTimer_E);
        }
    }

    void InitTime(ref float _cooltime)
    {
        _cooltime = 0f;
    }
}
