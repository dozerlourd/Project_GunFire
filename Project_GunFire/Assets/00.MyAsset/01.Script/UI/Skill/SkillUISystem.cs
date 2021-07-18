using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillUISystem : SceneObject<SkillUISystem>
{
    [SerializeField] Text skillCount_Q_txt;
    [SerializeField] Image SkillCooltime_E_img;
    [SerializeField] Image SkillCooltime_Dash_img;
    [SerializeField] Text SkillCooltime_Dash_txt;

    WaitForEndOfFrame waitForEndOfFrame = new WaitForEndOfFrame();

    PlayerSkill pSkill;

    private void Start()
    {
        pSkill = PlayerSystem.Instance.PlayerSkill;
        StartCoroutine(CheckSkillUI());
    }

    IEnumerator CheckSkillUI()
    {
        while (true)
        {
            skillCount_Q_txt.text = pSkill.SkillCount_Q.ToString();
            SkillCooltime_E_img.fillAmount = 1 - (pSkill.CurrTimer_E / pSkill.CoolTime_E);
            SkillCooltime_Dash_img.fillAmount = 1 - (PlayerSystem.Instance.PlayerMovement.CurrDashCooltime / PlayerSystem.Instance.PlayerMovement.DashCooltime);
            SkillCooltime_Dash_txt.text = ((int)(PlayerSystem.Instance.PlayerMovement.DashCooltime - PlayerSystem.Instance.PlayerMovement.CurrDashCooltime)).ToString();
            SkillCooltime_Dash_txt.enabled = 1 - (PlayerSystem.Instance.PlayerMovement.CurrDashCooltime / PlayerSystem.Instance.PlayerMovement.DashCooltime) > 0.1f;

            yield return waitForEndOfFrame;
        }
    }
}
