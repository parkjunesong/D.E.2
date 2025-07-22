using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Heal : Effect_Base
{
    public Effect_Heal(float value, int range, AType aimType, ATarget aimTarget) : base(value, range, aimType, aimTarget) { }
    
    public override void Execute(Unit caster, float CoE, int[] cost)
    {
        float heal = Value * CoE; //* (1 + caster.È¸º¹·® / 100);
        if (FieldEffectManager.Instance.GetcurrentEffectCost() != CostType.Null)
            heal *= (1 + cost[(int)FieldEffectManager.Instance.GetcurrentEffectCost()] * 3 / 100f);


        foreach (var target in setTarget(caster))
        {
            target.OnHealed(heal);
        }               
    }
}
