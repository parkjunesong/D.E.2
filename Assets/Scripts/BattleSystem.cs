using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : BattleGroup
{
    public static int Turn;

    void Start()
    {
        Turn = 0;
    }

    public void Rotation()
    {
        GameObject temp = RotaList[0];
        RotaList.RemoveAt(0);
        RotaList.Add(temp);

        TurnEnd();
    }

    public void TurnEnd()
    {
        Turn++;
        /*
        for (int i = 0; i < RotaList.Count; i++)
        {
            CGroup[i].GetComponent<BattleUnit>().TurnEnd_Before();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<BattleUnit>().TurnEnd_Before();
        }
        */
    }
}
