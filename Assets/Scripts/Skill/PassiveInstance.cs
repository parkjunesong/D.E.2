using UnityEngine;

public class PassiveInstance
{   
    public Skill_Passive PassiveSkill;
    public int CurrentStack;
    protected Unit Unit;

    public PassiveInstance(Skill_Passive passive, Unit unit)
    {
        Unit = unit;
        PassiveSkill = passive;
        CurrentStack = 0;
    }
    public virtual void OnTurnStart() { }
    public virtual void OnTurnEnd() { }
    public virtual void OnActionExecuted() { }
    public virtual void OnAttackExecuted(Unit target) { }
    public virtual void OnBuffGained(Buff_Base buff) { }
    public virtual void OnCostVanished() { }
    public virtual void OnDied() { }
}