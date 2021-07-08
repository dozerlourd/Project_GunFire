using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArousal : MonoBehaviour
{
    #region Elemental
    #endregion

    #region Smoke
    [Tooltip("���� Ȯ��: ����ź �������� 4/8/12�� �þ��. 3���� �Ǹ� ����ź�� 2�� ���� ������ ����ź 1���� ����ȴ�.")]
    int lv_BackpackExpension = 0;
    [Tooltip("�ص� �Ȱ�: ����ź�� ȭ���� 300/400/500% �þ��. 3���� �Ǹ� ���� 1�� ���� ȭ�� 100%�� �߰��� ����ȴ�.")]
    int lv_ToxicSmoke = 0;
    #endregion

    #region Orb
    [Tooltip("������ �ӹ�: ������ ������ ���� �ð��� 2/4/6�� �þ��. 3���� �Ǹ� ��Ÿ���� 30% �پ���.")]
    int lv_EnchantmentLast = 0;
    [Tooltip("���� ��õ: ������ ���긦 1/2/5�� �� �� �� �ִ�.")]
    int lv_PowerSource = 0;
    #endregion
}
