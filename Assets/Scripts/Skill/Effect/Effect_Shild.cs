using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Shild", menuName = "Scriptable Object/EffectData/Effect_Shild", order = int.MaxValue)]
public class Effect_Shild : Effect_Base
{
    public float Value;
    public override void Execute(Unit caster, List<Unit> targets)
    {
        float shild = Value * caster.Ability.AT; //* (1 + caster.È¸º¹·® / 100);
  
        foreach (var target in targets)
        {
            target.getShild(shild);
        }               
    }
}
