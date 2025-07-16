using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AType { Select, Front, Back, Random, Near, All, Self };
public enum ATarget { Player, Enemy };

public class Unit_Skill
{
    public List<Skill_Base> SkillList;

    public Unit_Skill(List<Skill_Base> skills)
    {
        SkillList = skills;
    }

    public List<Unit> setTargets(AType AimType, ATarget AimTarget)
    {
        BattleManager BM = BattleManager.Instance;
        if (AimTarget == ATarget.Enemy)
        {
            switch (AimType)
            {
                case AType.Select:
                    return new List<Unit> { BM.SelectedEnemyUnit };
                case AType.Front:
                    return new List<Unit> { BM.EnemyUnits[0] };
                case AType.Back:
                    return new List<Unit> { BM.EnemyUnits[BM.EnemyUnits.Count - 1] };
                case AType.Random:
                    return new List<Unit> { BM.EnemyUnits[Random.Range(0, BM.EnemyUnits.Count)] };
                case AType.Near:
                    return null;
                case AType.All:
                    return null;
                case AType.Self:
                    return null;
            }
        }
        else if (AimTarget == ATarget.Player)
        {
            switch (AimType)
            {
                case AType.Select:
                    return new List<Unit> { BM.SelectedPlayerUnit };
                case AType.Front:
                    return new List<Unit> { BM.alivePlayerUnits[0] };
                case AType.Back:
                    return new List<Unit> { BM.alivePlayerUnits[BM.alivePlayerUnits.Count - 1] };
                case AType.Random:
                    return new List<Unit> { BM.alivePlayerUnits[Random.Range(0, BM.alivePlayerUnits.Count)] };
                case AType.Near:
                    return null;
                case AType.All:
                    return null;
                case AType.Self:
                    return null;
            }
        }
        return null;
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
                List<List<Unit>> effectTargets = new List<List<Unit>>();

                foreach (var effect in skill.Effects)
                {
                    List<Unit> targets = new List<Unit>();

                    targets = setTargets(effect.AimType, effect.AimTarget);
                    effectTargets.Add(targets);
                }
                skill.Execute(caster, effectTargets);
                skill.ResetCooldown();
                CostManager.Instance.CostUse(SkillCost);
                BattleManager.Instance.TurnEnd();
            }
            else if(caster.Ability.Team == "Enemy")
            {
                List<List<Unit>> effectTargets = new List<List<Unit>>();

                foreach (var effect in skill.Effects)
                {
                    List<Unit> targets = new List<Unit>();

                    targets = setTargets(effect.AimType, effect.AimTarget);
                    effectTargets.Add(targets);
                }
                skill.Execute(caster, effectTargets);
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
