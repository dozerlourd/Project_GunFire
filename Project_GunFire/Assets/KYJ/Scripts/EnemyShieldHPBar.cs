using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyShieldHPBar : EnemyHPBar
{
    public Image shieldBar;
    
    // Update is called once per frame
    new void Update()
    {
        base.Update();

        shieldBar.fillAmount = enemyHP.CurrentShield / enemyHP.MaxShield;


    }
}
