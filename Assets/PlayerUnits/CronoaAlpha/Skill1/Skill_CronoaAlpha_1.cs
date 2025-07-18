using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Cronoa_Alpha_1", menuName = "Scriptable Object/SkillData/CronoaAlpha_1", order = int.MaxValue)]
public class Skill_CronoaAlpha_1 : Skill_Base
{
    public override void Execute(Unit caster, List<Unit> targets)
    {
        Effects[0].Execute(caster, targets); // Èû ¹öÇÁ È¹µæ
        caster.Passive.PassiveStack += 2;
    }
}