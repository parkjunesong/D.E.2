using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BuffModifier
{
    public float AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 
}

public abstract class Buff_Base : ScriptableObject
{
    public BuffModifier Modifier = new BuffModifier();
    public string Buff_Name;
    public Sprite Buff_Icon;
    public int Buff_Stack; // 버프 중첩 수, 매 턴마다 1 감소
    public int MaxStack;
    public int currentStack;
    protected bool wasAppliedThisTurn = false;

    public virtual void Apply() 
    {
        currentStack = Buff_Stack;
        wasAppliedThisTurn = true;
    }
    public virtual void AddStack()
    {
        currentStack += Buff_Stack;
        if (currentStack >= MaxStack)
            currentStack = MaxStack;
       
        wasAppliedThisTurn = true;
    }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() 
    {
        if (wasAppliedThisTurn) // 다음 턴부터 Tick 허용
        {
            wasAppliedThisTurn = false;
            return;
        }
        currentStack--;      
    }
    public virtual void Remove() 
    {
        currentStack = 0;
    }
}