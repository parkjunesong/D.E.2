using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : BattleGroup
{
    void Start()
    {
        int i = 0;
        foreach (GameObject unit in CGroup)
        {
            GameObject.Find("GameManager").GetComponent<UnitSpawn>().Spawn(unit, new Vector2(-500 + 30 * i, 1100));
            i++;
        }
        foreach (GameObject unit in EGroup)
        {
            GameObject.Find("GameManager").GetComponent<UnitSpawn>().Spawn(unit, new Vector2(500 + 30 * i, 1100));
            i++;
        }
    }
}
