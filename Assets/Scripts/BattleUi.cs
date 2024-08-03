using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUi : MonoBehaviour
{
    GameObject skill, cost, turn;
    void Start()
    {
        skill = GameObject.Find("스킬보드");
        cost = GameObject.Find("코스트보드");
        turn = GameObject.Find("Turn");
        uiReset();
    }
    public void uiReset()
    {
        Unit MC = gameObject.GetComponent<BattleSystem>().RotaList[0].GetComponent<Unit>();
        skill.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = MC.Skills[0].Skill_Icon;
        //skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[1].Skill_Icon;
        //skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[2].Skill_Icon;
        cost.GetComponent<CostSet>().CostReset();
        turn.GetComponent<Text>().text = BattleSystem.Turn + " Turn";
    }
    public void UseSkill(int i)
    {
        Unit MC = gameObject.GetComponent<BattleSystem>().RotaList[0].GetComponent<Unit>();
        MC.Attack(i);
    }
}
