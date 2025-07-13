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

    private List<Buff_Base> activeBuffs = new List<Buff_Base>();

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
        SystemManager.system.TurnEnd();
    }
    public void AddBuff(Buff_Base newBuff)
    {
        Buff_Base existing = activeBuffs.Find(buff => buff.Buff_Name == newBuff.Buff_Name);

        if (existing != null) existing.AddStack();
        else
        {
            Buff_Base instance = Instantiate(newBuff);
            instance.Apply(this);
            activeBuffs.Add(instance);
        }
        Ability.RecalculBuff(activeBuffs);
        Ui.UpdateBuffUI(activeBuffs);
    }
    public void Damaged(float damage, DType dT, int ignore)
    {
        Ability.Damaged(damage, dT, ignore);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void Healed(float heal)
    {
        Ability.Healed(heal);
        Ui.UpdateHPBar(Ability.HP, Data.HP);
    }
    public void TurnStart()
    {
        foreach (var buff in activeBuffs)
            buff.TurnStart(this);

        Ability.RecalculBuff(activeBuffs);
        Ui.UpdateBuffUI(activeBuffs);
    }
    public void TurnEnd()
    {
        for (int i = activeBuffs.Count - 1; i >= 0; i--) 
        {
            var buff = activeBuffs[i];
            buff.TurnEnd(this); 

            if (buff.currentStack <= 0)
            {
                buff.Remove(this);
                activeBuffs.RemoveAt(i);
            }    
        }
    }
}
