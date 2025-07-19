using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill_Base : ScriptableObject
{
    public int[] Skill_Cost;
    public int Skill_CoolTime;
    public int CurrentCoolTime = 0;
    public string Skill_Name;
    public Sprite Skill_Icon;
    public List<Effect_Base> EffectList = new();

    public abstract void SetEffect();
    public abstract void Execute(Unit caster);

    public void TickCoolTime()
    {
        if (CurrentCoolTime > 0)
            CurrentCoolTime--;
    }
    public void ResetCoolTime()
    {
        CurrentCoolTime = Skill_CoolTime + 1;
    }
    public void ReduceCoolTime(int reduce)
    {
        if (CurrentCoolTime > 0)
            CurrentCoolTime -= reduce;
        if (CurrentCoolTime <= 0)
            CurrentCoolTime = 0;
        SkillManager.Instance.uiReset();
    }
}