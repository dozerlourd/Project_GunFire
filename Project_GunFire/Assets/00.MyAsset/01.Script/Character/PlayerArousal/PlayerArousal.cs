using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArousal : MonoBehaviour
{
    #region Elemental
    #endregion

    #region Smoke
    public int BackpackExpension => lv_BackpackExpension;
    [Tooltip("���� Ȯ��: ����ź �������� 4/8/12�� �þ��. 3���� �Ǹ� ����ź�� 2�� ���� ������ ����ź 1���� ����ȴ�.")]
    int lv_BackpackExpension = 0;

    public int ToxicSmoke => lv_ToxicSmoke;
    [Tooltip("�ص� �Ȱ�: ����ź�� ȭ���� 300/400/500% �þ��. 3���� �Ǹ� ���� 1�� ���� ȭ�� 100%�� �߰��� ����ȴ�.")]
    int lv_ToxicSmoke = 0;
    #endregion

    #region Orb
    public int EnchantmentLast => lv_EnchantmentLast;
    [Tooltip("������ �ӹ�: ������ ������ ���� �ð��� 2/4/6�� �þ��. 3���� �Ǹ� ��Ÿ���� 30% �پ���.")]
    int lv_EnchantmentLast = 0;

    public int PowerSource => lv_PowerSource;
    [Tooltip("���� ��õ: ������ ���긦 1/2/5�� �� �� �� �ִ�.")]
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
            // ���� ���� 1�̸� ���� maxCount_Q + 4.
            if (BackpackExpension == 1) return maxCount_Q + 4;
            // �׷��� �ʰ�, ���� 2�̸� ���� maxCount_Q + 8.
            else if (BackpackExpension == 2) return maxCount_Q + 8;
            // �׷��� �ʰ�, ���� 3�̸� ���� maxCount_Q + 12.
            else if (BackpackExpension == 3) return maxCount_Q + 12;
            // �׷��� �ʰ�, ���� 2�̸� ���� maxCount_Q.
            else return maxCount_Q;
        }
    }
    [SerializeField, Tooltip("����ź �ִ� ������ (0���� ����)")] int maxCount_Q = 4;

    public float ChainReleasedTime
    {
        get
        {
            // ���� ���� 1�̸� ���� chainReleasedTime + 2.
            if (EnchantmentLast == 1) return chainReleasedTime + 2;
            // �׷��� �ʰ�, ���� 2�̸� ���� chainReleasedTime + 4.
            else if (EnchantmentLast == 2) return chainReleasedTime + 4;
            // �׷��� �ʰ�, ���� 3�̸� ���� chainReleasedTime + 6.
            else if (EnchantmentLast == 3) return chainReleasedTime + 6;
            // �׷��� �ʰ�, ���� ������ ���� chainReleasedTime.
            else return chainReleasedTime;
        }
    }
    [SerializeField, Tooltip("ü�� Ǯ���� �ð� (0���� ����)")] float chainReleasedTime = 3;

    public float ToxicDamage
    {
        get
        {
            // ���� ���� 1�̸� ���� chainReleasedTime + 2.
            if (ToxicSmoke == 1) return toxicDamage * 3.0f;
            // �׷��� �ʰ�, ���� 2�̸� ���� chainReleasedTime + 4.
            else if (ToxicSmoke == 2) return toxicDamage * 4.0f;
            // �׷��� �ʰ�, ���� 3�̸� ���� chainReleasedTime + 6.
            else if (ToxicSmoke == 3) return toxicDamage * 5.0f;
            // �׷��� �ʰ�, ���� ������ ���� chainReleasedTime.
            else return toxicDamage;
        }
    }
    [SerializeField, Tooltip("ü�� Ǯ���� �ð� (0���� ����)")] float toxicDamage = 130;
}
