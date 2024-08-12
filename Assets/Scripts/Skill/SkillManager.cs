using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager skill;
    public GameObject SkillUi;

    void Awake()
    {
        skill = this;
        SkillUi = GameObject.Find("스킬보드");
    }
    public void UseSkill(Skill_Base skill, Unit_Ablity ability)
    {
        int[] SkillCost = skill.Skill_Cost;
        int[] CostNow = CostManager.cost.GetComponent<CostManager>().CostCount();

        if (CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2]))
        {
            skill.execute(ability);
            CostManager.cost.GetComponent< CostManager> ().CostUse(SkillCost);
        }
        else
            Debug.Log("Can't Use Skill");
    }
    public void MainCharaUseSkill(int i)
    {
        SystemManager.system.MainChara.GetComponent<Unit>().Attack(i);
    }

    public void uiReset()
    {
        Unit unit = SystemManager.system.MainChara.GetComponent<Unit>();
        SkillUi.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = unit.Skills[0].Skill_Icon;
        //skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[1].Skill_Icon;
        //skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[2].Skill_Icon;
    }
}
