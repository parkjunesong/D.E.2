using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Passive : ScriptableObject
{
    public string Name;
    public Sprite Icon;   
    public int MaxStack;
    public List<Effect_Base> EffectList = new();

    public abstract PassiveInstance CreateInstance(Unit caster);
    public abstract void SetEffect();
    public abstract void Execute(Unit caster);
}