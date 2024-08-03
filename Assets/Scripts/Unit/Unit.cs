using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Unit: Unit_Base
{
    public List<Skill_Base> Skills = new List<Skill_Base>();
    public Unit_Ablity Ability;
    public Unit_Animation Animation;

    public string Unit_Name;
    int AT, SP, HP, DF;
    float CR, CD, RD, ID;
    // 능력치 원본

    public SkillManager SC;
    Slider HpBar;

    void Awake()
    {
        Transform[] allChildren = transform.GetChild(0).GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildren.Length; i++)
        {
            Skill_Base skill = allChildren[i].GetComponent<Skill_Base>();
            Skills.Add(skill);
        }
        
        SC = GameObject.Find("GameManager").GetComponent<SkillManager>();
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
        SC.UseSkill(Skills[i], Ability); 
    }
    public override void Damaged(float damage, DType dT, int ignore)
    {
        Ability.Damaged(damage, dT, ignore);
        HpBar.value = (float)Ability.HP / HP;
    }
}
