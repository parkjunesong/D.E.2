using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum UnitType { Unit, Summoned, Object };
public class Unit: Unit_Base
{
    public List<Skill_Base> Skills = new List<Skill_Base>();
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public UnitType Type;

    int AT, SP, HP, DF;
    float CR, CD, RD, ID;
    // 능력치 원본

    private Slider HpBar;

    void Awake()
    {
        Transform[] allChildren = transform.GetChild(0).GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildren.Length; i++)
        {
            Skill_Base skill = allChildren[i].GetComponent<Skill_Base>();
            Skills.Add(skill);
        }
        
        HpBar = gameObject.transform.GetChild(1).GetChild(0).GetComponent<Slider>();
        Ability = gameObject.GetComponent<Unit_Ablity>();
        Animation = gameObject.GetComponent<Unit_Animation>();

        AT = Ability.AT;
        SP = Ability.SP;
        HP = Ability.HP;
        DF = Ability.DF;
        CR = Ability.CR;
        CD = Ability.CD;
        RD = Ability.RD;
        ID = Ability.ID;
    }
    public override void Attack(int i)
    {
        SkillManager.skill.UseSkill(Skills[i], Ability); 
    }
    public override void Damaged(float damage, DType dT, int ignore)
    {
        Ability.Damaged(damage, dT, ignore);
        HpBar.value = (float)Ability.HP / HP;
    }
    public override void TurnStart()
    {
        foreach (Skill_Base skill in Skills) skill.TurnStart();
    }
    public override void TurnEnd()
    {
        foreach (Skill_Base skill in Skills) skill.TurnEnd();
    }
}
