using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cronoa_Alpha_Passive", menuName = "Scriptable Object/SkillData/CronoaAlpha_Passive")]
public class Skill_CronoaAlpha_Passive : Skill_Base
{   
    public override void SetEffect() { }
    public override void Execute(Unit caster)
    {
        foreach (var unit in UnitManager.Instance.alivePlayerUnits)
            foreach (var skill in unit.Skill.SkillList)
                skill.ReduceCoolTime(unit, caster.Passive.PassiveStack);
    }
}