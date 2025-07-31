using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CastingSkill
{
    private Unit Caster;
    private Skill_Base Skill;
    private int Skill_CastingTime;
    private int currentCastingTime;

    public CastingSkill(Unit caster, Skill_Base skill, int castingtime)
    {
        Caster = caster;
        Skill = skill;
        Skill_CastingTime = castingtime;
        currentCastingTime = 0;
    }
    public void TickCastingTime()
    {
        if (currentCastingTime < Skill_CastingTime) 
        {
            Caster.Ui.ShowFloatingText("Casting:" + (Skill_CastingTime - currentCastingTime), Color.magenta);
            currentCastingTime++;
        }
        else
        {
            currentCastingTime = 0;
            Skill.CastingExecute(Caster);
        }
    }
}


public abstract class Skill_Base : ScriptableObject
{
    public string Skill_Name;
    public Sprite Skill_Icon;
    public List<Effect_Base> EffectList = new();
    public int[] Skill_Cost;
    public int Skill_CoolTime;
    public int currentCoolTime;

    public virtual bool IsAvailable(Unit caster) { return true; } // 특수 발동조건 체크
    public abstract void SetEffect();
    public abstract void Execute(Unit caster);
    public virtual void CastingExecute(Unit caster) { } // 캐스팅 스킬일 경우, 효과 작성



    public void TickCoolTime()
    {
        if (currentCoolTime > 0) currentCoolTime--;
    }  
    public void ResetCoolTime()
    {
        currentCoolTime = Skill_CoolTime + 1;
    }
    public void ReduceCoolTime(Unit target, int reduce)
    {
        if(target.Ability.Team == "Player")
        {
            currentCoolTime -= reduce;
            if (currentCoolTime <= 0) currentCoolTime = 0;
            SkillManager.Instance.uiReset();
        }
        else if (target.Ability.Team == "Enemy")
        {
            target.GetComponent<EnemyUnit>().MoveCount -= reduce;
            if (target.GetComponent<EnemyUnit>().MoveCount <= 0) target.GetComponent<EnemyUnit>().MoveCount = 0;
        }
        target.Ui.ShowFloatingText("Reduce:" + reduce, Color.blue);

    }
    public void DelayCoolTime(Unit target, int delay)
    {
        if (target.Ability.Team == "Player")
        {
            currentCoolTime += delay;
            SkillManager.Instance.uiReset();
        }
        else if (target.Ability.Team == "Enemy")
        {
            target.GetComponent<EnemyUnit>().MoveCount += delay;
        }
        target.Ui.ShowFloatingText("Delay:" + delay, Color.blue);
    }
}