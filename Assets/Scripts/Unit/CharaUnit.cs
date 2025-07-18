using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Position
{
    Front,
    Middle,
    Back
}

public class CharaUnit : Unit
{
    public Position Position;

    public override void Init()
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
    public override void TurnStart()
    {
        Skill.OnTurnStart();
        Buff.OnTurnStart();
    }
    public override void TurnEnd()
    {
        Skill.OnTurnEnd();
        Buff.OnTurnEnd();
    }
}