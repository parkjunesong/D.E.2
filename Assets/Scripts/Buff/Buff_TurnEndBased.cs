using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buff_TurnEndBased : Buff_Base
{
    private bool wasAppliedThisTurn = false;

    public override void OnApply(int stack) 
    {
        base.OnApply(stack);
        wasAppliedThisTurn = true; 
    }
    public override void AddBuff(int stack)
    {
        base.AddBuff(stack);
        wasAppliedThisTurn = true;
    }
    public override void OnTurnEnd()
    {
        if (wasAppliedThisTurn) // 다음 턴부터 Tick 허용
        {
            wasAppliedThisTurn = false;
            return;
        }
        SubBuff(1);
    }
}