using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Iris_3", menuName = "Scriptable Object/SkillData/Iris_3")]
public class Skill_Iris_3 : Skill_Base
{
    public List<Skill_Base> newSkills = new();
    public List<Skill_Base> temp;
    bool isMetamorphosis;

    public override bool IsAvailable(Unit caster)
    {
        if (((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack < 5) return false;
        else return true;
    }
    public override void SetEffect()
    {
        Effect_Base effect1 = new Effect_Damage(2.0f, 10, AType.Select, ATarget.Enemy, DType.Normal, 0);
        EffectList.Add(effect1);
        isMetamorphosis = false;
    }
    public override void Execute(Unit caster)
    {
        if (!isMetamorphosis)
        {
            EffectList[0].Execute(caster, caster.Ability.AT, Skill_Cost);

            caster.Ability.Element = Element.Void;
            temp = caster.Skill.SkillList;
            caster.Skill.SkillList = newSkills;
            //Skill_Cost[2] = 0;
        }
        else
        {
            Effect_Base effect = new Effect_Damage(2.0f + 2 * ((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack, 10, AType.Select, ATarget.Enemy, DType.Normal, 20 + ((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack);
            effect.Execute(caster, caster.Ability.AT, Skill_Cost);

            caster.Ability.Element = Element.Creation;
            caster.Skill.SkillList = temp;
            //Skill_Cost[2] = 5;
            ((PassiveInstance_Iris)caster.Skill.Passive).skill3_Stack = 0;
        }
        isMetamorphosis = !isMetamorphosis;
    } 
}