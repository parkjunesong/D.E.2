using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit: Unit_Base
{
    public string Unit_Name;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public SkillManager SC;
    public string[] Skills = new string[3];

    void Awake()
    {
        SC = GameObject.Find("GameManager").GetComponent<SkillManager>();
        Ability = gameObject.GetComponent<Unit_Ablity>();
        Animation = gameObject.GetComponent<Unit_Animation>();
    }
    public override void Attack(int i)
    {
        Skill_Base skill = SC.GetSkill(Skills[i]);
        SC.UseSkill(skill); 
    }
    public override void Damaged(int damage)
    {
        
    }
}
