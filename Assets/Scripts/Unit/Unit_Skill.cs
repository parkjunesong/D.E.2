using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Skill
{
    public List<Skill_Base> SkillList;
    public int currentSkillNo;
    public int prevSkillNo;

    public Unit_Skill(List<Skill_Base> skills)
    {
        SkillList = skills;
        currentSkillNo = -1;
        prevSkillNo = -1;
    }

    public void UseSkill(int i, Unit caster, bool forced = false)
    {
        Skill_Base skill = SkillList[i];
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = CostManager.Instance.CostCount();

        if (forced) // 스킬 발동 조건 없이 사용
        {
            currentSkillNo = i;
            caster.OnActionExcuted();
            skill.Execute(caster);
            prevSkillNo = i;
        }
        else
        {
            if ((CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2])))
            {
                if (caster.Ability.Team == "Player" && skill.currentCoolTime <= 0 && skill.IsAvailable(caster))
                {
                    currentSkillNo = i;
                    caster.OnActionExcuted();
                    skill.Execute(caster);
                    skill.ResetCoolTime();
                    prevSkillNo = i;
                    CostManager.Instance.CostUse(SkillCost);
                    BattleManager.Instance.TurnEnd();
                }
                else if (caster.Ability.Team == "Enemy")
                {
                    currentSkillNo = i;
                    caster.OnActionExcuted();
                    skill.Execute(caster);
                    skill.ResetCoolTime();
                    prevSkillNo = i;
                    CostManager.Instance.CostUse(SkillCost);
                }
            }
            else
                Debug.Log("Can't Use Skill");
        }       
    }
    public void OnTurnStart() { }
    public void OnTurnEnd() 
    {
        foreach (var skill in SkillList)
        {
            skill.TickCoolTime();
        }
    }
}
