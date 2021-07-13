using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHP : HPController
{
    [SerializeField] GameObject damageText;

    public override float TakeDamage
    {
        set
        {
            DamageText _damageText = Instantiate(damageText, transform.position, Quaternion.identity).GetComponent<DamageText>();
            _damageText.SetDamageText((int)value);

            if (CurrentSheild <= 0)
            {
                CurrentHP -= value;
            }
            else if (CurrentSheild - value <= 0)
            {
                float temp = value -= CurrentSheild;
                CurrentSheild = 0;
                CurrentHP -= temp;
            }
            else if (CurrentSheild - value > 0)
            {
                SheildDamage = value;
            }
        }
    }

    private new void Start()
    {
        base.Start();
    }

    public override float SheildDamage { set => CurrentSheild -= value; }

    protected override bool Damage(ref float setHP) => false;

    protected override void Death(ref float setHP) { }

    protected override bool OverHeal(ref float setHP) => false;

    protected override bool RecoveryHeal(ref float setHP) => false;

    protected override void RefreshUIElement() { }
}
