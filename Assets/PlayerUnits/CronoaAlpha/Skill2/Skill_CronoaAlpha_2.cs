using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Skill_Cronoa_Alpha_2", menuName = "Scriptable Object/SkillData/CronoaAlpha_2", order = int.MaxValue)]
public class Skill_CronoaAlpha_2 : Skill_Base
{
    public override void Execute(Unit caster, List<Unit> targets)
    {
        Effects[0].Execute(caster, targets); // 0.4AT 관통공격
        for(int i = 0; i< caster.Passive.PassiveStack; i++)
        {
            Effects[1].Execute(caster, targets); // 0.2AT * '시간의 톱니바퀴' 관통공격
        }
    }
}