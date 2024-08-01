using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUi : MonoBehaviour
{
    GameObject skill, cost, turn;
    void Start()
    {
        //skill = GameObject.Find("스킬보드");
        cost = GameObject.Find("코스트보드");
        turn = GameObject.Find("Turn");
        uiReset();
    }
    public void uiReset()
    {
        /*
        skill.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_스킬1", typeof(Sprite)) as Sprite;
        skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_스킬2", typeof(Sprite)) as Sprite;
        skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_스킬3", typeof(Sprite)) as Sprite;
        */
        cost.GetComponent<CostSet>().CostReset();
        turn.GetComponent<Text>().text = BattleSystem.Turn + " Turn";
    }


    int[] SkillCost;
    int[] CostNow;
    public void useskill1()
    {
        CostNow = cost.GetComponent<CostSet>().CostCount();
        SkillCost = new int[] { 1, 1, 0 };
        if (CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2]))
            cost.GetComponent<CostSet>().CostUse(SkillCost);
        else
            Debug.Log("Can't Use Skill");
    }
    public void useskill2()
    {
        CostNow = cost.GetComponent<CostSet>().CostCount();
        SkillCost = new int[] { 0, 0, 1 };
        if (CostNow[0] >= SkillCost[0] && CostNow[1] >= SkillCost[1] && (CostNow[2] >= SkillCost[2] || CostNow[3] >= SkillCost[2]))
            cost.GetComponent<CostSet>().CostUse(SkillCost);
        else
            Debug.Log("Can't Use Skill");
    }
}
