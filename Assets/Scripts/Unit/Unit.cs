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
    public UnitUi Ui;
    public int GroupNo; // Enemy 사망시 GroupNo 갱신 필요

    void Awake()
    {       
        Ability = new Unit_Ablity(Data);                     
        Animation = new Unit_Animation(Data);
        Ui = transform.GetChild(0).GetComponent<UnitUi>();

        Animation.Default(gameObject);
    }
    public void Skill(int i)
    {
        SkillManager.skill.UseSkill(Skills[i], this); 
    }
    public void Damaged(float damage, DType dT, int ignore)
    {
        Ability.Damaged(damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void TurnStart()
    {
        //foreach (Skill_Base skill in Skills) skill.TurnStart();
    }
    public void TurnEnd()
    {
        //foreach (Skill_Base skill in Skills) skill.TurnEnd();
    }
}
