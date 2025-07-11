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

    public virtual void Apply(Unit target) 
    {
        currentStack = Buff_Stack;
    }
    public virtual void AddStack()
    {
        currentStack = Mathf.Min(currentStack + 1, MaxStack);
    }
    public virtual void TurnStart(Unit target) { }
    public virtual void TurnEnd(Unit target) 
    {
        currentStack--;      
    }
    public virtual void Remove(Unit target) 
    {
        currentStack = 0;
    }
}