using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUi : MonoBehaviour
{
    GameObject skill, cost;
    void Start()
    {
        //skill = GameObject.Find("스킬보드");
        cost = GameObject.Find("코스트보드");
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
    }
    public void useskill()
    {
        cost.GetComponent<CostSet>().CostUse(new int[] { 1, 2, 0 });
    }
}
