using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : BattleGroup
{
    public static int Turn;
    BattleUi ui;

    void Start()
    {
        Turn = 0;
        ui = gameObject.GetComponent<BattleUi>();
        TurnStart();
    }

    public void Rotation()
    {
        GameObject temp = RotaList[0];
        RotaList.RemoveAt(0);
        RotaList.Add(temp);

        TurnEnd();
    }

    public void TurnStart()
    {
        Turn++;
        ui.uiReset();

        for (int i = 0; i < RotaList.Count; i++)
        {
            RotaList[i].GetComponent<Unit>().TurnStart();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnStart();
            EGroup[i].GetComponent<Enemy_Base>().TurnStart();
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
            EGroup[i].GetComponent<Enemy_Base>().TurnEnd();
        }
        TurnStart();
    }
}
