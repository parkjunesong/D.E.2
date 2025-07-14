using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public ScenarioData sData;

    private void Start()
    {
        int i;
        //sData = ScenarioManager.Instance.CurrentScenario;

        if (sData != null)
        {
            i = 0;
            foreach (UnitData data in sData.mData.PlayerUnitData)
            {
                UnitData uData = Instantiate(data);
                BattleManager.Instance.UnitSpawn(uData, new Vector2(-200 - 280 * i, 1100), "Player", i);               
                i++;
            }
            i = 0;
            foreach (UnitData data in sData.mData.EnemyUnitData)
            {
                UnitData uData = Instantiate(data);
                BattleManager.Instance.UnitSpawn(uData, new Vector2(200 + 280 * i, 1100), "Enemy", i);
                i++;
            }
            BattleManager.Instance.BattleStart();
        }
    }  
}
