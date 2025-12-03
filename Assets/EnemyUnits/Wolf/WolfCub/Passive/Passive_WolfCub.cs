using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PassiveInstance_WolfCub : PassiveInstance
{
    public PassiveInstance_WolfCub(Skill_Passive passive, Unit caster) : base(passive, caster) { }

    public override void OnDied()
    {
        PassiveSkill.Execute(Unit);
    }   
}