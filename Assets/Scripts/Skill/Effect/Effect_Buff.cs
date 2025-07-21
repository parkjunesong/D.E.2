using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Buff : Effect_Base
{
    public Buff_Base Buff;
    public int Stack;

    public Effect_Buff(float value, int range, AType aimType, ATarget aimTarget, Buff_Base buff, int stack) : base(value, range, aimType, aimTarget)
    {
        Buff = buff;
        Stack = stack;
    }

    public override void Execute(Unit caster)
    {
        foreach (var target in setTarget(caster))
        {
            target.OnBuffGained(Buff, Stack);
        }
    }
}
