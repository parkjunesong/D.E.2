using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff_Simple", menuName = "Scriptable Object/Buff/Buff_Simple", order = int.MaxValue)]
public class Buff_StackType : Buff_Base
{
    /* Stack Type Buff
     * Duration = currentStack
     * Level = currentStack
     */
    public int maxStack;
    private int currentStack;
    private bool wasAppliedThisTurn = false;

    public override void Apply()
    {
        currentStack = Level;
        Duration = currentStack;
        wasAppliedThisTurn = true;
    }
    public override void AddStack(int stack)
    {
        currentStack += stack;
        if (currentStack >= maxStack)
            currentStack = maxStack;

        Level = currentStack;
        Duration = currentStack;
        wasAppliedThisTurn = true;
    }
    public override void TurnStart() { }
    public override void TurnEnd()
    {
        if (wasAppliedThisTurn) // 다음 턴부터 Tick 허용
        {
            wasAppliedThisTurn = false;
            return;
        }
        currentStack--;
        Level = currentStack;
        Duration = currentStack;
    }   
}