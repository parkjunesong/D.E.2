using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitState
{
    Alive,       // 기본 상태
    Dead,        // 사망
    Summon,      // 소환된 유닛 (소환수 등)
    Structure    // 구조물 (타겟팅 불가, 이동 불가 등)
}

[System.Serializable]
public class Ability
{
    public float AT, SP, HP, DF, CR, CD, RD, ID;
    public Ability()
    {
        AT = 0;
        SP = 0;
        HP = 0;
        DF = 0;
        CR = 0;
        CD = 0;
        RD = 0;
        ID = 0;
    }
    public Ability(int at, int sp, int hp, int df, float cr, float cd, float rd, float id)
    {       
        AT = at;
        SP = sp;
        HP = hp;
        DF = df;
        CR = cr;
        CD = cd;
        RD = rd;
        ID = id;
    }
}

public class Unit_Ablity
{
    public string Name;
    public Element Element; // 플레이어 유닛의 속성, 다속성 플레이어 유닛의 경우 속성 교체 필요
    public List<Element> Elements;
    public ElementReactionState ReactionState = new ElementReactionState();
    public int AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public int Shild;
    public int maxHP;
    public string Team;
    public UnitState State;   

    protected UnitData Data;
    protected Ability BuffModifiers;

    public Unit_Ablity(UnitData data)
    {
        Data = data;
        Name = data.Name;
        Elements = data.Elements;
        Element = Elements[0];
        AT = data.AT;
        SP = data.SP;
        HP = data.HP;
        maxHP = HP;
        DF = data.DF;
        CR = data.CR;
        CD = data.CD;
        RD = data.RD;
        ID = data.ID;
        Shild = 0;
        State = UnitState.Alive;
    }
      
    public void OnDamaged(Unit unit, float damage, DType dT, int ignore)
    {
        float dam = damage * 100 / (100 + DF * (1f - ignore / 100));
        switch (dT)
        {
            case DType.Normal:
                {
                    unit.Ui.ShowFloatingText(((int)(dam * (1.0f - RD / 100))).ToString(), Color.red);

                    if (Shild > 0)
                    {
                        Shild -= (int)(dam * (1.0f - RD / 100));
                        if (Shild <= 0)
                        {
                            HP -= -Shild;
                            Shild = 0;
                        }
                        break;
                    }
                    else
                    {
                        HP -= (int)(dam * (1.0f - RD / 100));
                        break;
                    }         
                }
            case DType.Penetrate:
                {
                    unit.Ui.ShowFloatingText(((int)(dam * (1.0f - RD / 100))).ToString(), Color.red);

                    HP -= (int)(dam * (1.0f - RD / 100));
                    break;
                }
            case DType.True: 
                {
                    unit.Ui.ShowFloatingText(((int)damage).ToString(), Color.red);

                    HP -= (int)damage;
                    break;
                }
        }

        
        if (HP <= 0)
            HP = 0;          
    }

    public void OnHealed(Unit unit, float heal)
    {
        unit.Ui.ShowFloatingText(heal.ToString(), Color.green);

        if (heal + HP < maxHP)
            HP += (int)(heal);
        else
            HP = maxHP;
    }
    public void OnShieldGained(Unit unit, float shild)
    {
        unit.Ui.ShowFloatingText(shild.ToString(), Color.yellow);
        Shild += (int)(shild);
    }

    public void RecalculBuff(List<Buff_Base> buffs)
    {
        BuffModifiers = new Ability();

        foreach (var buff in buffs)
        {
            BuffModifiers.AT += buff.BuffEffect.AT * buff.Level;
            BuffModifiers.DF += buff.BuffEffect.DF * buff.Level;
            BuffModifiers.SP += buff.BuffEffect.SP * buff.Level;
            BuffModifiers.HP += buff.BuffEffect.HP * buff.Level;
            BuffModifiers.CR += buff.BuffEffect.CR * buff.Level;
            BuffModifiers.CD += buff.BuffEffect.CD * buff.Level;
            BuffModifiers.RD += buff.BuffEffect.RD * buff.Level;
            BuffModifiers.ID += buff.BuffEffect.ID * buff.Level;

            AT = (int)(Data.AT * (1 + BuffModifiers.AT / 100));
            SP = (int)(Data.SP * (1 + BuffModifiers.SP));
            maxHP = (int)(Data.HP * (1 + BuffModifiers.HP / 100));
            DF = (int)(Data.DF * (1 + BuffModifiers.DF / 100));
            CR = Data.CR + BuffModifiers.CR;
            CD = Data.CD + BuffModifiers.CD;
            RD = Data.RD + BuffModifiers.RD;
            ID = Data.ID + BuffModifiers.ID;
        }
    }
}
