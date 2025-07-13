using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Heal", menuName = "Scriptable Object/EffectData/Effect_Heal", order = int.MaxValue)]
public class Effect_Heal : Effect_Base
{
    public float Value;
    public override void Execute(Unit caster, List<Unit> targets)
    {
        float heal = Value * caster.Ability.AT; //* (1 + caster.È¸º¹·® / 100);
  
        foreach (var target in targets)
        {
            target.OnHealed(heal);
        }               
    }
}
