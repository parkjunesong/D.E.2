using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class SystemManager : BattleGroup
{
    public static SystemManager system;
    public int Turn;
    GameObject TurnUi;

    void Awake()
    {
        system = this;
    }
    void Start()
    {
        foreach(GameObject chara in CGroup)
        {
            Unit unit = chara.GetComponent<Unit>();
            if (unit.Ability.UT == UnitType.Unit_Alive)
            {
                RotaList.Add(chara);
            }
        }

        MainChara = RotaList[0];

        Turn = 0;
        TurnUi = GameObject.Find("Turn");

        TurnStart();
    }
    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        SkillManager.skill.uiReset();

        for (int i = 0; i < CGroup.Count; i++)
        {
            CGroup[i].GetComponent<Unit>().TurnStart();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnStart();
        }
    }
    public void TurnEnd()
    {
        for (int i = 0; i < CGroup.Count; i++)
        {
            CGroup[i].GetComponent<Unit>().TurnEnd();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnEnd();
        }
        TurnStart();
    }
    public void Rotation()
    {
        GameObject temp = RotaList[0];
        RotaList.RemoveAt(0);
        RotaList.Add(temp);

        int j = 0;     
        for (int i = 0; i < CGroup.Count; i++)
        {
            Unit unit = CGroup[i].GetComponent<Unit>();
            if (unit.Ability.UT == UnitType.Unit_Alive)
            {
                CGroup[i] = RotaList[j];
                j++;
            }
        }
        foreach(GameObject chara in CGroup)
        {
            Unit unit = chara.GetComponent<Unit>();
            unit.GroupNo = CGroup.IndexOf(chara);
            chara.transform.position = new Vector2(-200 - 280 * unit.GroupNo, 1100);
        }

        //SelectedChara = unit.GroupNo;
        MainChara = RotaList[0];

        CostManager.cost.CostReset();
        TurnEnd();
    }


}
