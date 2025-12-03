using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInstance_Iris : PassiveInstance
{
    public int skill3_Stack = 0;
    public PassiveInstance_Iris(Skill_Passive passive, Unit caster) : base(passive, caster) { }
    
    public override void OnActionExecuted()
    {
        if (Unit.Skill.prevSkillNo == -1) CurrentStack++;
        else if (Unit.Skill.currentSkillNo == Unit.Skill.prevSkillNo && Unit.Skill.currentSkillNo != 2)
        {
            CurrentStack++;
            if (CurrentStack > PassiveSkill.MaxStack)
            {
                CurrentStack = 0;
                PassiveSkill.Execute(Unit);
            }
        }
        else CurrentStack = 0;
    }
    public override void OnCostVanished()
    {
        skill3_Stack++;
        if (skill3_Stack > 10) skill3_Stack = 10;
    }
}