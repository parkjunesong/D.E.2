using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Iris : Unit_Passive
{
    public int skill3_Stack = 0;
    public override void OnActionExecuted()
    {
        if(Unit.Skill.prevSkillNo == -1) PassiveStack++;
        else if (Unit.Skill.currentSkillNo == Unit.Skill.prevSkillNo && Unit.Skill.currentSkillNo != 2)
        {
            PassiveStack++;
            if (PassiveStack > MaxStack)
            {
                PassiveStack = 0;
                Execute();
            }
        }
        else
        {
            PassiveStack = 0;
        }
    }
    public override void OnCostVanished()
    {
        skill3_Stack++;
        if (skill3_Stack > 10)
        {
            skill3_Stack = 10;;
        }
    }
}