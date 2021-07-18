using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHPBar : MonoBehaviour
{
    public Image HpBar_;

    public Image shieldBar;

 

    // Update is called once per frame
    protected void Update()
    {
        HpBar_.fillAmount = PlayerSystem.Instance.PlayerHP.CurrentHP / PlayerSystem.Instance.PlayerHP.MaxHP;

        shieldBar.fillAmount = PlayerSystem.Instance.PlayerHP.CurrentShield / PlayerSystem.Instance.PlayerHP.MaxShield;
    }
}
