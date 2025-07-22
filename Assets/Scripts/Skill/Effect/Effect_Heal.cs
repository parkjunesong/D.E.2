using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Heal : Effect_Base
{
    public Effect_Heal(float value, int range, AType aimType, ATarget aimTarget) : base(value, range, aimType, aimTarget) { }
    
    public override void Execute(Unit caster, float CoE, int[] cost)
    {
        float heal = Value * CoE * (1 + (GetFieldEffectCoE(cost)) / 100); // 회복량 계수 추가바람


        foreach (var target in setTarget(caster))
        {
            target.OnHealed(heal);
        }               
    }
}
