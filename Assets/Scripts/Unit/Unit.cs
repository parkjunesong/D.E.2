using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Unit : MonoBehaviour
{
    public UnitData Data;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    public List<Skill_Base> Skills = new List<Skill_Base>();
    UnitUi Ui;

    void Awake()
    {
        Transform[] allChildren = transform.GetChild(0).GetComponentsInChildren<Transform>();
        for (int i = 1; i < allChildren.Length; i++)
        {
            Skill_Base skill = allChildren[i].GetComponent<Skill_Base>();
            Skills.Add(skill);
        }

        Ability = new Unit_Ablity(Data);                     
        Animation = new Unit_Animation(Data);
        Ui = transform.GetChild(1).GetComponent<UnitUi>();

        Animation.Default(gameObject);
    }
    public void Attack(int i)
    {
        SkillManager.skill.UseSkill(Skills[i], Ability); 
    }
    public void Damaged(float damage, DType dT, int ignore)
    {
        Ability.Damaged(damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void TurnStart()
    {
        foreach (Skill_Base skill in Skills) skill.TurnStart();
    }
    public void TurnEnd()
    {
        foreach (Skill_Base skill in Skills) skill.TurnEnd();
    }
}
