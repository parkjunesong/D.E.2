using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit_Passive : MonoBehaviour
{
    protected Unit Unit;
    public Skill_Base PassiveSkill;
    public int PassiveStack;
    public int MaxStack;
    void Awake()
    {
        Unit = transform.parent.GetComponent<Unit>();
    }
    public void Execute()
    {
        PassiveSkill.Execute(Unit);
    }
    public virtual void OnTurnStart()
    {

    }
    public virtual void OnTurnEnd()
    {

    }
    public virtual void OnAttackExecuted(Unit target)
    {

    }
    public virtual void OnBuffGained(Buff_Base buff)
    {

    }
}
