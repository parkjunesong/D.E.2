using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleSystem : BattleGroup
{
    public static int Turn;
    GameObject TurnUi;

    void Start()
    {
        Turn = 0;
        TurnUi = GameObject.Find("Turn");
        TurnStart();
    }
    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        SkillManager.skill.uiReset();

        for (int i = 0; i < RotaList.Count; i++)
        {
            RotaList[i].GetComponent<Unit>().TurnStart();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnStart();
        }
    }
    public void TurnEnd()
    {
        for (int i = 0; i < RotaList.Count; i++)
        {
            RotaList[i].GetComponent<Unit>().TurnEnd();
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

        CostManager.cost.CostReset();
        TurnEnd();
    }
}
