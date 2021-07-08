using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArousal : MonoBehaviour
{
    #region Elemental
    #endregion

    #region Smoke
    [Tooltip("가방 확장: 수류탄 소지량이 4/8/12개 늘어난다. 3렙이 되면 수류탄을 2개 던질 때마다 수류탄 1개가 재생된다.")]
    int lv_BackpackExpension = 0;
    [Tooltip("극독 안개: 연막탄의 화력이 300/400/500% 늘어난다. 3렙이 되면 폭발 1초 마다 화력 100%가 추가로 적용된다.")]
    int lv_ToxicSmoke = 0;
    #endregion

    #region Orb
    [Tooltip("강력한 속박: 에너지 오브의 지속 시간이 2/4/6초 늘어난다. 3렙이 되면 쿨타임이 30% 줄어든다.")]
    int lv_EnchantmentLast = 0;
    [Tooltip("힘의 원천: 에너지 오브를 1/2/5번 더 쏠 수 있다.")]
    int lv_PowerSource = 0;
    #endregion
}
