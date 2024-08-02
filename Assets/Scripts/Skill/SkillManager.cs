using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public List<Skill_Base> Skills = new List<Skill_Base>();
    private Dictionary<string, Skill_Base> SD;

    GameObject cost;

    void Awake()
    { 
        cost = GameObject.Find("코스트보드");
        SD = new Dictionary<string, Skill_Base>();
        foreach(Skill_Base skill in Skills)
        {
            SD.Add(skill.Skill_Name, skill);
        }
    }
    public Skill_Base GetSkill(string Name)
    {
        if (SD.TryGetValue(Name, out Skill_Base skill)) return skill;
        return null;
    }

    public void UseSkill(Skill_Base skill)
    {
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = cost.GetComponent<CostSet>().CostCount();

        if (CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2]))
        {
            //skill.execute();
            cost.GetComponent<CostSet>().CostUse(SkillCost);
        }
        else
            Debug.Log("Can't Use Skill");
    }
}
