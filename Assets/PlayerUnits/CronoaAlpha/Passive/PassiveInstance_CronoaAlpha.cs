using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveInstance_CronoaAlpha : PassiveInstance
{
    public PassiveInstance_CronoaAlpha(Skill_Passive passive, Unit caster) : base(passive, caster) { }
    public override void OnTurnStart()
    {
        CurrentStack += 1;
        if(Unit.GetComponent<PlayerUnit>().Position == Position.Front)
            CurrentStack += 1;
        if (CurrentStack > PassiveSkill.MaxStack)
        {
            CurrentStack -= PassiveSkill.MaxStack;
            PassiveSkill.Execute(Unit);
            CurrentStack = PassiveSkill.MaxStack;
        }
    }   
}