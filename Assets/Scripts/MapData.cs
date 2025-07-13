using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapData : MonoBehaviour
{
    public List<UnitData> CGroup = new List<UnitData>();
    public List<UnitData> EGroup = new List<UnitData>();
    void Awake()
    {
        int i;
        i = 0;
        foreach (UnitData data in CGroup)
        {
            UnitData uData = Instantiate(data);
            gameObject.GetComponent<UnitSpawn>().Spawn(uData, new Vector2(-200 - 280 * i, 1100), "Chara");
            i++;
        }
        i = 0;
        foreach (UnitData data in EGroup)
        {
            UnitData uData = Instantiate(data);
            gameObject.GetComponent<UnitSpawn>().Spawn(uData, new Vector2(200 + 280 * i, 1100), "Enemy");
            i++;
        }
    }
}
