using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum AType { Select, Front, Back, Random, Near, All, Self};
public enum ATarget { Chara, Enemy };

public class SkillManager : MonoBehaviour
{
    public static SkillManager skill;
    public GameObject SkillUi;

    void Awake()
    {
        skill = this;
        SkillUi = GameObject.Find("스킬보드");
    }
    public List<Unit> setTargets(AType AimType, ATarget AimTarget)
    {
        if (AimTarget == ATarget.Enemy)
        {
            switch (AimType)
            {
                case AType.Select: 
                    return new List<Unit> { SystemManager.system.EGroup[SystemManager.system.SelectedEnemy].GetComponent<Unit>() };
                case AType.Front: 
                    return new List<Unit> { SystemManager.system.EGroup[0].GetComponent<Unit>()};
                case AType.Back: 
                    return new List<Unit> { SystemManager.system.EGroup[SystemManager.system.EGroup.Count - 1].GetComponent<Unit>() };
                case AType.Random: 
                    return new List<Unit> { SystemManager.system.EGroup[Random.Range(0, SystemManager.system.EGroup.Count)].GetComponent<Unit>() };
                case AType.Near: 
                    return null;
                case AType.All: 
                    return null;
                case AType.Self: 
                    return null;
            }
        }
        else if (AimTarget == ATarget.Chara)
        {
            switch (AimType)
            {
                case AType.Select:
                    return new List<Unit> { SystemManager.system.CGroup[SystemManager.system.SelectedChara].GetComponent<Unit>() };
                case AType.Front:
                    return new List<Unit> { SystemManager.system.CGroup[0].GetComponent<Unit>() };
                case AType.Back:
                    return new List<Unit> { SystemManager.system.CGroup[SystemManager.system.CGroup.Count - 1].GetComponent<Unit>() };
                case AType.Random:
                    return new List<Unit> { SystemManager.system.CGroup[Random.Range(0, SystemManager.system.CGroup.Count)].GetComponent<Unit>() };
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

    public void UseSkill(Skill_Base skill, Unit caster)
    {
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = CostManager.cost.GetComponent<CostManager>().CostCount();

        if (CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2]))
        {
            List<List<Unit>> effectTargets = new List<List<Unit>>();

            foreach (var effect in skill.Effects)
            {
                List<Unit> targets = new List<Unit>();

                targets = setTargets(effect.AimType, effect.AimTarget);
                effectTargets.Add(targets);
            }
            skill.Execute(caster, effectTargets);

            CostManager.cost.GetComponent< CostManager> ().CostUse(SkillCost);
        }
        else
            Debug.Log("Can't Use Skill");
    }
    
    public void MainCharaUseSkill(int i)
    {
        SystemManager.system.MainChara.GetComponent<Unit>().Skill(i);
    }
    public void uiReset()
    {
        Unit unit = SystemManager.system.MainChara.GetComponent<Unit>();
        SkillUi.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = unit.Skills[0].Skill_Icon;
        //skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[1].Skill_Icon;
        //skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[2].Skill_Icon;
    }
}
