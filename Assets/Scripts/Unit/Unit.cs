using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

public abstract class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public Unit_Skill Skill;
    public Unit_Ui Ui;
    public Unit_Buff Buff;
    public Unit_Passive Passive;

    public abstract void Init();
    public abstract void TurnStart();
    public abstract void TurnEnd();
    public void OnActionExcuted() // 행동 당 한번 호출(스킬 발동 시 호출)
    {
        ElementManager.Instance.OnActionExcuted(this);
        Buff.OnActionExcuted();
        Passive.OnActionExecuted();
    }
    public void OnAttackExecuted(Unit target) // 매 타격마다 호출
    {
        Passive.OnAttackExecuted(target);
    }
    public void OnDamaged(float damage, DType dT, int ignore) // 피격 시 호출
    {
        Ability.OnDamaged(this, damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);

        if (Ability.HP <= 0)
            OnDied();
    }
    public void OnBuffGained(Buff_Base newBuff, int count) // 버프 획득 시 호출
    {
        Buff.OnBuffGained(newBuff, count);
        Passive.OnBuffGained(newBuff);
    }
    public void OnHealed(float heal) // 체력 회복 시 호출
    {
        Ability.OnHealed(this, heal);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void OnShieldGained(float shild) // 실드 획득 시 호출
    {
        Ability.OnShieldGained(this, shild);
        Ui.UpdateShildBar(Ability.Shild, Ability.maxHP);
    }
    public void OnDied()
    {
        //버프 삭제, 능력치 초기화, ui 원복
        Passive.OnDied();
        Animation.Dead();
        UnitManager.Instance.OnUnitDied(this);
    }
}
