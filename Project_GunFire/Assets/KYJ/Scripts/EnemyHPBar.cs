using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHPBar : MonoBehaviour
{
    // 태어날 때 체력을 최대체력으로
    // 적이 플레이어 공격할 때 체력 감소
    // 체력이 변경되면 ui에도 반영하고싶다


    public Image HpBar;

    public EnemyHP enemyHP;
    


    // Start is called before the first frame update
  

    // Update is called once per frame
    protected void Update()
    {
        HpBar.fillAmount = enemyHP.CurrentHP/enemyHP.MaxHP;


       
    }
}
