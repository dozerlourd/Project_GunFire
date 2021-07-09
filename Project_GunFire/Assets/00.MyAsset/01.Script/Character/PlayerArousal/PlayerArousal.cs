using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArousal : MonoBehaviour
{
    #region Elemental
    #endregion

    #region Smoke
    public int BackpackExpension => lv_BackpackExpension;
    [Tooltip("가방 확장: 수류탄 소지량이 4/8/12개 늘어난다. 3렙이 되면 수류탄을 2개 던질 때마다 수류탄 1개가 재생된다.")]
    int lv_BackpackExpension = 0;

    public int ToxicSmoke => lv_ToxicSmoke;
    [Tooltip("극독 안개: 연막탄의 화력이 300/400/500% 늘어난다. 3렙이 되면 폭발 1초 마다 화력 100%가 추가로 적용된다.")]
    int lv_ToxicSmoke = 0;
    #endregion

    #region Orb
    public int EnchantmentLast => lv_EnchantmentLast;
    [Tooltip("강력한 속박: 에너지 오브의 지속 시간이 2/4/6초 늘어난다. 3렙이 되면 쿨타임이 30% 줄어든다.")]
    int lv_EnchantmentLast = 0;

    public int PowerSource => lv_PowerSource;
    [Tooltip("힘의 원천: 에너지 오브를 1/2/5번 더 쏠 수 있다.")]
    int lv_PowerSource = 0;
    #endregion

    public void UpgradeBackpackExpension() => Mathf.Clamp(lv_BackpackExpension++, 0, 3);
    public void UpgradeToxicSmoke() => Mathf.Clamp(lv_ToxicSmoke++, 0, 3);
    public void UpgradeEnchantmentLast() => Mathf.Clamp(lv_EnchantmentLast++, 0, 3);
    public void UpgradePowerSource() => Mathf.Clamp(lv_PowerSource++, 0, 3);

    private void Start()
    {
        UpgradeToxicSmoke();
        UpgradeToxicSmoke();
        UpgradeToxicSmoke();
    }

    public int MaxCount_Q
    {
        get
        {
            // 만약 각성 1이면 리턴 maxCount_Q + 4.
            if (BackpackExpension == 1) return maxCount_Q + 4;
            // 그렇지 않고, 각성 2이면 리턴 maxCount_Q + 8.
            else if (BackpackExpension == 2) return maxCount_Q + 8;
            // 그렇지 않고, 각성 3이면 리턴 maxCount_Q + 12.
            else if (BackpackExpension == 3) return maxCount_Q + 12;
            // 그렇지 않고, 각성 2이면 리턴 maxCount_Q.
            else return maxCount_Q;
        }
    }
    [SerializeField, Tooltip("수류탄 최대 소지량 (0각성 기준)")] int maxCount_Q = 4;

    public float ChainReleasedTime
    {
        get
        {
            // 만약 각성 1이면 리턴 chainReleasedTime + 2.
            if (EnchantmentLast == 1) return chainReleasedTime + 2;
            // 그렇지 않고, 각성 2이면 리턴 chainReleasedTime + 4.
            else if (EnchantmentLast == 2) return chainReleasedTime + 4;
            // 그렇지 않고, 각성 3이면 리턴 chainReleasedTime + 6.
            else if (EnchantmentLast == 3) return chainReleasedTime + 6;
            // 그렇지 않고, 각성 없으면 리턴 chainReleasedTime.
            else return chainReleasedTime;
        }
    }
    [SerializeField, Tooltip("체인 풀리는 시간 (0각성 기준)")] float chainReleasedTime = 3;

    public float ToxicDamage
    {
        get
        {
            // 만약 각성 1이면 리턴 chainReleasedTime + 2.
            if (ToxicSmoke == 1) return toxicDamage * 3.0f;
            // 그렇지 않고, 각성 2이면 리턴 chainReleasedTime + 4.
            else if (ToxicSmoke == 2) return toxicDamage * 4.0f;
            // 그렇지 않고, 각성 3이면 리턴 chainReleasedTime + 6.
            else if (ToxicSmoke == 3) return toxicDamage * 5.0f;
            // 그렇지 않고, 각성 없으면 리턴 chainReleasedTime.
            else return toxicDamage;
        }
    }
    [SerializeField, Tooltip("체인 풀리는 시간 (0각성 기준)")] float toxicDamage = 130;
}
