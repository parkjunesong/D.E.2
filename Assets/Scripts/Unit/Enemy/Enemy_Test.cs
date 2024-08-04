using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Test : Enemy_Base
{
    public override void Awake()
    {
        base.Awake();
        MoveCount = 3;
    }
    public override void TurnStart()
    {
        base.TurnStart();
    }
    public override void TurnEnd()
    {
        if (MoveCount == 0) 
        {
            unit.Attack(0);
            MoveCount = 3;
        }
    }
}
