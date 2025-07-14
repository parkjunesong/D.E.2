using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Position
{
    Front,
    Middle,
    Back,
    Support
}

public class BattleUnit
{
    public GameObject GameObject;       
    public Unit Unit;
    public Position Position;
    public bool IsAlive;

    public BattleUnit(GameObject obj)
    {
        GameObject = obj;
        Unit = obj.GetComponent<Unit>();       
        IsAlive = true;
    }
}