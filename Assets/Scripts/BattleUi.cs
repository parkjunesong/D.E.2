using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUi : MonoBehaviour
{
    GameObject skill, cost, turn;
    void Start()
    {
        //skill = GameObject.Find("��ų����");
        cost = GameObject.Find("�ڽ�Ʈ����");
        turn = GameObject.Find("Turn");
        uiReset();
    }
    public void uiReset()
    {
        /*
        skill.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_��ų1", typeof(Sprite)) as Sprite;
        skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_��ų2", typeof(Sprite)) as Sprite;
        skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = Resources.Load("Icon/" + MainChara.GetComponent<BattleUnit>().Data.Get_UnitData.Get_Name + "_��ų3", typeof(Sprite)) as Sprite;
        */
        cost.GetComponent<CostSet>().CostReset();
        turn.GetComponent<Text>().text = BattleSystem.Turn + " Turn";
    }
}
