using System;
using UnityEngine;

public abstract class HPController : MonoBehaviour
{
    bool isDeath = false;

    [SerializeField] float m_maxHP;
    [SerializeField] float m_maxShield;
    float m_currentHP;
    float m_currentShield;

    public float MaxHP { get => m_maxHP; protected set => m_maxHP = value; }

    public float MaxShield { get => m_maxShield; protected set => m_maxShield = value; }

    public float CurrentHP
    {
        get => m_currentHP; set
        {
            if (isDeath) return;
            if (m_currentHP < value) if(RecoveryHeal(ref value)) return;
            if (MaxHP < value) if(OverHeal(ref value)) return;
            if (m_currentHP > value) if(Damage(ref value)) return;
            m_currentHP = value; RefreshUIElement();
            if (m_currentHP <= 0)
            {
                Death(ref value);
                isDeath = true;
            }
        }
    }

    public float CurrentShield
    {
        get => m_currentShield; set
        {
            m_currentShield = value;
        }
    }

    protected void Start()
    {
        CurrentHP = MaxHP;
        CurrentShield = MaxShield;
    }

    protected virtual void Generation()
    {
        isDeath = false;
        m_currentHP = MaxHP;
        RefreshUIElement();
    }

    public abstract float TakeDamage { set; }

    public abstract float SheildDamage { set; }

    protected abstract bool RecoveryHeal(ref float setHP);
    protected abstract bool OverHeal(ref float setHP);
    protected abstract bool Damage(ref float setHP);
    protected abstract void RefreshUIElement();
    protected abstract void Death(ref float setHP);
}
