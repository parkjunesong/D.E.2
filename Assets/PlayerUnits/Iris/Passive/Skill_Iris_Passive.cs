using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

[CreateAssetMenu(fileName = "Skill_Iris_Passive", menuName = "Scriptable Object/SkillData/Iris_Passive", order = int.MaxValue)]
public class Skill_Iris_Passive : Skill_Base
{  
    public override void SetEffect()
    {

    }
    public override void Execute(Unit caster)
    {
        caster.Skill.UseSkill(caster.Skill.currentUsedSkillNo, caster, true);    
    }
}