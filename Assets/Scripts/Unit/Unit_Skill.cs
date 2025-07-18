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

    public List<Unit> setTarget(int Range, AType AimType, ATarget AimTarget, Unit caster)
    {
        BattleManager BM = BattleManager.Instance;
        if (AimTarget == ATarget.Enemy)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(BM.SelectedEnemyUnit, Range, BM.EnemyUnits);
                case AType.Front:
                    return GetNearUnits(BM.EnemyUnits[0], Range, BM.EnemyUnits);
                case AType.Back:
                    return GetNearUnits(BM.EnemyUnits[BM.EnemyUnits.Count - 1], Range, BM.EnemyUnits); 
                case AType.Random:
                    return GetNearUnits(BM.EnemyUnits[Random.Range(0, BM.EnemyUnits.Count)], Range, BM.EnemyUnits);         
                case AType.Self:
                    return new List<Unit>() { caster };
            }
        }
        else if (AimTarget == ATarget.Player)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(BM.SelectedPlayerUnit, Range, BM.alivePlayerUnits); 
                case AType.Front:
                    return GetNearUnits(BM.alivePlayerUnits[0], Range, BM.alivePlayerUnits); 
                case AType.Back:
                    return GetNearUnits(BM.alivePlayerUnits[BM.alivePlayerUnits.Count - 1], Range, BM.alivePlayerUnits); 
                case AType.Random:
                    return GetNearUnits(BM.alivePlayerUnits[Random.Range(0, BM.alivePlayerUnits.Count)], Range, BM.alivePlayerUnits);    
                case AType.Self:
                    return new List<Unit>() { caster };
            }
        }
        return null;
    }
    public List<Unit> GetNearUnits(Unit middle, int range, List<Unit> group)
    {
        List<Unit> nearby = new List<Unit>();

        if (range == 0) 
        {
            nearby.Add(middle);
        }
        else
        {
            for (int i = -range; i <= range; i++)
            {
                int targetIndex = group.IndexOf(middle) + i;

                if (targetIndex >= 0 && targetIndex < group.Count)
                    nearby.Add(group[targetIndex]);
            }
        }      
        return nearby;
    } 

    public void OnUseSkill(int i, Unit caster)
    {
        Skill_Base skill = SkillList[i];
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = CostManager.Instance.CostCount();

        if ((CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2])))
        {
            if(caster.Ability.Team == "Player" && skill.CurrentCooldown <= 0)
            {
                List<Unit> targets = new();
                foreach (var effect in skill.Effects)
                {
                    targets = setTarget(effect.Range, effect.AimType, effect.AimTarget, caster);
                }
                skill.Execute(caster, targets);
                skill.ResetCooldown();
                CostManager.Instance.CostUse(SkillCost);
                BattleManager.Instance.TurnEnd();
            }
            else if(caster.Ability.Team == "Enemy")
            {
                List<Unit> targets = new List<Unit>();
                foreach (var effect in skill.Effects)
                {
                    targets = setTarget(effect.Range, effect.AimType, effect.AimTarget, caster);
                }
                skill.Execute(caster, targets);
                skill.ResetCooldown();
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
            skill.TickCooldown();
    }
}
