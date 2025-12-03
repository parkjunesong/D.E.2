using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Position
{
    Front,
    Middle,
    Back
}

public class PlayerUnit : Unit
{
    public Position Position;

    public override void Init()
    {
        List<Skill_Base> Skills = new();
        foreach(Skill_Base skill in Data.Skills)
        {
            Skill_Base instance = Instantiate(skill);
            instance.SetEffect();
            Skills.Add(instance);
        }
        Skill = new Unit_Skill(Skills, Instantiate(Data.Passive), this);
        Ability = new Unit_Ablity(Data);
        Animation = new Unit_Animation(this);
        Ui = gameObject.AddComponent<Unit_Ui>();
        Buff = gameObject.AddComponent<Unit_Buff>();
    }
    public override void TurnStart()
    {
        Skill.OnTurnStart();
        Buff.OnTurnStart();
        if (Skill.Passive != null) Skill.Passive.OnTurnStart();
    }
    public override void TurnEnd()
    {
        Skill.OnTurnEnd();
        Buff.OnTurnEnd();
        if (Skill.Passive != null) Skill.Passive.OnTurnEnd();

    }
}