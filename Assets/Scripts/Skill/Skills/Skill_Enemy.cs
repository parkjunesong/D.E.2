using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Simple", menuName = "Scriptable Object/SkillData/Skill_Simple", order = int.MaxValue)]
public class Skill_Simple : Skill_Base
{
    public override void Execute(Unit caster, List<List<Unit>> effectTargets)
    {
        for (int i = 0; i < Effects.Length; i++)
        {
            Effects[i].Execute(caster, effectTargets[i]);
        }       
    }
}