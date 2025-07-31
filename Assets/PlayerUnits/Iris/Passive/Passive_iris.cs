using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Iris : Unit_Passive
{
    public override void OnActionExecuted()
    {
        if(Unit.Skill.prevSkillNo == -1) PassiveStack++;
        else if (Unit.Skill.currentSkillNo == Unit.Skill.prevSkillNo)
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
}