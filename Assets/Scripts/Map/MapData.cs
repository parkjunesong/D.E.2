using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapData", menuName = "Scriptable Object/MapData", order = int.MaxValue)]

public class MapData : ScriptableObject
{
    //bgm, image 등 구현 바람
    public List<UnitData> CGroup = new List<UnitData>();
    public List<UnitData> EGroup = new List<UnitData>();   
}
