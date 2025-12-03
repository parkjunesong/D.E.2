using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInstance_Noa: PassiveInstance
{
    int Count = 0;
    public PassiveInstance_Noa(Skill_Passive passive, Unit caster) : base(passive, caster) { }


    public override void OnAttackExecuted(Unit target)
    {        
        if(CurrentStack >= 10) // ÆÐ½Ãºê´Â ÁßÃ¸ ¾È½×ÀÓ
        {
            Unit.OnActionExcuted();
            Effect_Damage damage = new Effect_Damage(0.1f, 0, AType.Select, ATarget.Enemy, DType.True, 0);
            target.OnDamaged(damage.getDamage(Unit, Unit.Ability.AT, new int[] { 0, 0, 0 }), damage.DamageType, 0);
        }
        if (Count < 1) Count++;
        else
        {
            Count = 0;
            CurrentStack++;
            if (CurrentStack > PassiveSkill.MaxStack) CurrentStack = PassiveSkill.MaxStack;
        }
    }
    public override void OnBuffGained(Buff_Base buff)
    {
        if (buff.Name == "Strangth") PassiveSkill.Execute(Unit);
    }
}