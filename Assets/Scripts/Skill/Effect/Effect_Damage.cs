using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DType { Normal, Penetrate, True };

[CreateAssetMenu(fileName = "Effect_Damage", menuName = "Scriptable Object/EffectData/Effect_Damage", order = int.MaxValue)]
public class Effect_Damage : Effect_Base
{
    public DType DamageType;
    public int IgnoreDefence = 0;

    public override void Execute(Unit caster, Unit target)
    {
        float damage = Value * caster.Ability.AT * (1 + caster.Ability.ID / 100);
        if (caster.Ability.CR >= Random.Range(0, 100)) damage *= (1 + caster.Ability.CD / 100);

        target.Damaged(damage, DamageType, IgnoreDefence);           
    }
}
