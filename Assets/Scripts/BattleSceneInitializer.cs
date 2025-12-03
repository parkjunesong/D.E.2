using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleSceneInitializer : MonoBehaviour
{
    public ScenarioData sData;

    private void Start()
    {
        int i;
        if (sData != null)
        {
            i = 0;
            foreach (UnitData data in sData.mData.PlayerUnitData)
            {
                UnitManager.Instance.Spawn(data, i, "Player");               
                i++;
            }
            i = 0;
            foreach (UnitData data in sData.mData.EnemyUnitData)
            {
                UnitManager.Instance.Spawn(data, i, "Enemy");
                i++;
            }
            BattleManager.Instance.BattleStart();
        }
    }  
}
