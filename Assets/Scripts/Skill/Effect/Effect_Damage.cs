using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DType { Normal, Penetrate, True };

public class Effect_Damage : Effect_Base
{
    public DType DamageType;
    public int IgnoreDefence = 0;

    public Effect_Damage(float value, int range, AType aimType, ATarget aimTarget, DType damageType, int ignoreDefence) : base(value, range, aimType, aimTarget)
    {
        DamageType = damageType;
        IgnoreDefence = ignoreDefence;
    }

    public override void Execute(Unit caster, float CoE, int[] cost)
    {
        foreach (var target in setTarget(caster))
        {
            ElementManager.Instance.Resolve(caster, target);
            ElementManager.Instance.OnDamaged(caster, target);
            target.OnDamaged(getDamage(caster, CoE, cost), DamageType, IgnoreDefence);
            caster.OnAttackExecuted(target);
        }               
    }
    public float getDamage(Unit caster, float CoE, int[] cost)
    {
        float damage = Value * CoE * (1 + (caster.Ability.ID + GetFieldEffectCoE(cost)) / 100);
        if (caster.Ability.CR >= Random.Range(0, 100)) 
            damage *= (1 + caster.Ability.CD / 100);


        return damage;
    }
}
