using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHP : HPController
{
    bool isDamaged = false;
    [SerializeField] float Seconds, HealCooltime;
    WaitForSeconds waitSeconds;
    WaitForSeconds waitHealCooltime;
    Coroutine co_DamageCheck;

    [SerializeField] float repaireShieldScale = 5.0f;
    Vector3 originPos;

    public override float TakeDamage
    {
        set
        {
            if (co_DamageCheck != null) StopCoroutine(co_DamageCheck);
            co_DamageCheck = StartCoroutine(DamageCheck());
            if (CurrentShield <= 0)
            {
                CurrentHP -= value;
            }
            else if (CurrentShield - value <= 0)
            {
                float temp = value -= CurrentShield;
                CurrentShield = 0;
                CurrentHP -= temp;
            }
            else if (CurrentShield - value > 0)
            {
                SheildDamage = value;
            }
        }
    }

    IEnumerator DamageCheck()
    {
        isDamaged = true;
        yield return waitSeconds;
        isDamaged = false;
    }

    new void Start()
    {
        base.Start();
        originPos = transform.position;
        waitSeconds = new WaitForSeconds(Seconds);
        waitHealCooltime = new WaitForSeconds(HealCooltime);
        StartCoroutine(ShieldRepair());
    }

    IEnumerator ShieldRepair()
    {  
        while(true)
        {
            yield return new WaitUntil(() => !isDamaged);
            yield return new WaitUntil(() => CurrentShield < MaxShield);
            CurrentShield += repaireShieldScale;
            yield return waitHealCooltime;
        }
    }

    public override float SheildDamage { set => CurrentShield -= value; }

    protected override bool Damage(ref float setHP) => false;

    protected override void Death(ref float setHP)
    {
        transform.position = originPos;
        ResetHP();
    }

    protected override bool OverHeal(ref float setHP) => false;

    protected override bool RecoveryHeal(ref float setHP) => false;

    protected override void RefreshUIElement() { }
}