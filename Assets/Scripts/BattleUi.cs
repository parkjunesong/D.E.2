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
}
