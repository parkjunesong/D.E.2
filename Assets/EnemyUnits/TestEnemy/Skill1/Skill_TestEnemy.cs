using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "TestEnemy_Skill", menuName = "Scriptable Object/SkillData/TestEnemy_Skill", order = int.MaxValue)]
public class Skill_TestEnemy : Skill_Base
{
    public override void Execute(Unit caster, List<Unit> targets)
    {
        Effects[0].Execute(caster, targets); // АјАн      
    }
}