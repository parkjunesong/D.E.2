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
    public string Name;
    public Sprite Icon;
    public int Level;
    public int Duration;
    public BuffModifier Modifier = new BuffModifier();
    // 제거불가 속성 추가 예정

    public virtual void Apply() { }
    public virtual void AddStack(int stack) { }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() 
    {
        Duration--;
    }
    public virtual void Remove()
    {
        Level = 0;
        Duration = 0;
    }
}