using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Ablity Ability;
    public Unit_Skill Skill;
    public Unit_Animation Animation;  
    public Unit_Ui Ui;
    public Unit_Buff Buff;

    void Awake()
    {       
        Ability = new Unit_Ablity(Data);
        Skill = new Unit_Skill(Data);
        Animation = new Unit_Animation(this);
        Ui = gameObject.AddComponent<Unit_Ui>();
        Buff = gameObject.AddComponent<Unit_Buff>();
    }

    public virtual void TurnStart()
    {
        Skill.OnTurnStart();
        Buff.OnTurnStart();
    }
    public virtual void TurnEnd()
    {
        Skill.OnTurnEnd();
        Buff.OnTurnEnd();
    }

    public void OnSkillUsed(int i)
    {
        Skill.OnUseSkill(i, this);
        SystemManager.system.TurnEnd();
    }
    public void OnBuffGained(Buff_Base newBuff)
    {
        Buff.OnBuffGained(newBuff);
    }
    public void OnDamaged(float damage, DType dT, int ignore)
    {
        Ability.OnDamaged(damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
        Ui.UpdateShildBar(Ability.Shild, Ability.HP);
    }
    public void OnHealed(float heal)
    {
        Ability.OnHealed(heal);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void OnShieldGained(float shild)
    {
        Ability.OnShieldGained(shild);
        Ui.UpdateShildBar(Ability.Shild, Ability.HP);
    }
}
