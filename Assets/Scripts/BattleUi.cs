using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUi : MonoBehaviour
{
    GameObject skill, cost;
    void Start()
    {
        //skill = GameObject.Find("��ų����");
        cost = GameObject.Find("�ڽ�Ʈ����");
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
    }
    public void useskill()
    {
        cost.GetComponent<CostSet>().CostUse(new int[] { 1, 2, 0 });
    }
}
