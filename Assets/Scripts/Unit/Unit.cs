using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public Unit_Skill Skill;
    public Unit_Ui Ui;
    public Unit_Buff Buff;

    public virtual void Init()
    {
        List<Skill_Base> Skills = new List<Skill_Base>();
        foreach (Skill_Base skill in Data.Skills)
        {
            Skill_Base instance = Instantiate(skill);
            Skills.Add(instance);
        }
        Skill = new Unit_Skill(Skills);

        Ability = new Unit_Ablity(Data);
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
    public virtual void Died() 
    {
        //버프 삭제, 능력치 초기화, ui 원복
        Animation.Dead();
        BattleManager.Instance.OnUnitDied(this);
    }

    public void OnSkillUsed(int i)
    {
        Skill.OnUseSkill(i, this);
    }
    public void OnBuffGained(Buff_Base newBuff)
    {
        Buff.OnBuffGained(newBuff);
    }
    public void OnDamaged(float damage, DType dT, int ignore)
    {
        Ability.OnDamaged(this, damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);

        if (Ability.HP <= 0)
            Died();
    }
    public void OnHealed(float heal)
    {
        Ability.OnHealed(this, heal);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void OnShieldGained(float shild)
    {
        Ability.OnShieldGained(this, shild);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);
    }
}
