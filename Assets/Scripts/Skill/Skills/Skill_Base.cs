using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill_Base : ScriptableObject
{
    public int[] Skill_Cost;
    public int Skill_CoolTime;
    public string Skill_Name;
    public Sprite Skill_Icon;
    public Effect_Base[] Effects;

    public abstract void Execute(Unit caster, List<List<Unit>> effectTargets);
}