using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    public List<GameObject> RotaList = new List<GameObject>();
    BattleGroup BG;
    public int Turn;
    int rota;

    void Start()
    {
        rota = 0;
        Turn = 0;
        BG = gameObject.GetComponent<BattleGroup>();
        foreach (GameObject unit in BG.CGroup) RotaList.Add(unit);     
    }

    public void Rotation()
    {
        rota++;
        if (rota >= RotaList.Count) rota = 0;

        BG.MainChara = RotaList[rota];
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
