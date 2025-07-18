using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Unit_Skill
{
    public List<Skill_Base> SkillList;

    public Unit_Skill(List<Skill_Base> skills)
    {
        SkillList = skills;
    }

    public void OnUseSkill(int i, Unit caster)
    {
        Skill_Base skill = SkillList[i];
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = CostManager.Instance.CostCount();

        if ((CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2])))
        {
            if (caster.Ability.Team == "Player" && skill.CurrentCoolTime <= 0)
            {
                skill.Execute(caster);
                skill.ResetCoolTime();
                CostManager.Instance.CostUse(SkillCost);
                BattleManager.Instance.TurnEnd();
            }
            else if (caster.Ability.Team == "Enemy")
            {                              
                skill.Execute(caster);
                skill.ResetCoolTime();
                CostManager.Instance.CostUse(SkillCost);
            }
        }
        else
            Debug.Log("Can't Use Skill");
    }
    public void OnTurnStart() { }
    public void OnTurnEnd() 
    {
        foreach (var skill in SkillList)
            skill.TickCoolTime();
    }
}
