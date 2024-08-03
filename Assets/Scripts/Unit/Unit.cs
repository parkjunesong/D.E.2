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
    public List<Skill_Base> Skills = new List<Skill_Base>();
    void Awake()
    {
        Transform[] allChildren = transform.GetChild(0).GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildren.Length; i++)
        {
            Skill_Base skill = allChildren[i].GetComponent<Skill_Base>();
            Skills.Add(skill);
        }
        
        SC = GameObject.Find("GameManager").GetComponent<SkillManager>();
        Ability = gameObject.GetComponent<Unit_Ablity>();
        Animation = gameObject.GetComponent<Unit_Animation>();
    }
    public override void Attack(int i)
    {
        Skill_Base skill = Skills[i];
        SC.UseSkill(skill); 
    }
    public override void Damaged(int damage)
    {
        
    }
}
