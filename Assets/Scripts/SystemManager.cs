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
                UnitSpawnManager.Instance.Spawn(data, i, "Player");               
                i++;
            }
            i = 0;
            foreach (UnitData data in sData.mData.EnemyUnitData)
            {
                UnitSpawnManager.Instance.Spawn(data, i, "Enemy");
                i++;
            }
            BattleManager.Instance.BattleStart();
        }
    }  
}
