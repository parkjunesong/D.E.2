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
        if(PassiveStack >= 10) // ÆÐ½Ãºê´Â ÁßÃ¸ ¾È½×ÀÓ
        {
            Unit.OnActionPerformed();
            Effect_Damage damage = new Effect_Damage(0.1f, 0, AType.Select, ATarget.Enemy, DType.True, 0);
            target.OnDamaged(damage.getDamage(Unit, Unit.Ability.AT, new int[] { 0, 0, 0 }), damage.DamageType, 0);
        }
        if (passiveCount < 1)
            passiveCount++;
        else
        {
            passiveCount = 0;
            PassiveStack++;
            if (PassiveStack > MaxStack)
                PassiveStack = MaxStack;
        }
    }
    public override void OnBuffGained(Buff_Base buff)
    {
        if (buff.Name == "Strangth")
        {
            Execute();
        }
    }
}