using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Buff_Base : ScriptableObject
{
    public Ability BuffEffect;
    public string Name;
    public Sprite Icon;
    public int Level;
    public int maxLevel;
    public virtual void OnApply(int stack) { Level = stack; }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void OnActionExcuted(Unit unit) { }
    public virtual void AddBuff(int stack)
    {
        Level += stack;
        if (Level >= maxLevel)
            Level = maxLevel;
    }
    public virtual void SubBuff(int stack)
    {
        Level -= stack;
        if (Level < 0)
            Level = 0;
    }
    public void Remove()
    {
        Level = 0;
    }
}