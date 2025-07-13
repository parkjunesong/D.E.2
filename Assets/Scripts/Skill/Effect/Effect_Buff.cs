using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Effect_Buff", menuName = "Scriptable Object/EffectData/Effect_Buff", order = int.MaxValue)]
public class Effect_Buff : Effect_Base
{
    public Buff_Base Buff;
    public override void Execute(Unit caster, List<Unit> targets)
    {
        foreach (var target in targets)
        {
            target.OnBuffGained(Buff);
        }
    }
}
