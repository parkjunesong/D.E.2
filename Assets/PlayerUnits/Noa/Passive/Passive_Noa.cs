using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Noa : Unit_Passive
{
    int passiveCount = 0;

    public override void OnAttackExecuted(Unit target)
    {
        if (passiveCount < 1)
            passiveCount++;
        else
        {
            PassiveStack++;
            passiveCount = 0;
        }
        Debug.Log(passiveCount + " / 패시브스택: " + PassiveStack);

        if(PassiveStack >= 1)
        {
            Effect_Damage damage = new Effect_Damage(0.1f, 0, AType.Select, ATarget.Enemy, DType.True);
            target.OnDamaged(damage.getDamage(Unit), damage.DamageType, 0);
        }
    }
    public override void OnBuffGained(Buff_Base buff)
    {
        if (buff.Name == "Buff_Strangth")
        {
            Execute();
        }
    }
}