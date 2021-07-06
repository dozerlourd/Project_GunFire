using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] GameObject ChainSphere;
    [SerializeField] float SkillMoveSpeed_E;

    [SerializeField] int MaxCount_Q;
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
            yield return SkillCountCheck_Q;
            yield return InputCheck_Q;

            SetSkillCount_Q(SkillCount_Q - 1);
            Debug.Log("Q");
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
