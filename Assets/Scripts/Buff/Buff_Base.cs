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
    public bool isPermanent;
    public string Name;
    public Sprite Icon;
    public int Level;
    public int Duration;
    public BuffModifier Modifier = new BuffModifier();

    public abstract void Apply();
    public virtual void AddStack(int stack) { }
    public virtual void SubStack(int stack) { }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() { }   
    public virtual void Remove()
    {
        Level = 0;
        Duration = 0;
    }
}