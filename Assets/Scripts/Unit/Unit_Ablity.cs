using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public enum UnitState
{
    Alive,       // 기본 상태
    Dead,        // 사망
    Summon,      // 소환된 유닛 (소환수 등)
    Structure    // 구조물 (타겟팅 불가, 이동 불가 등)
}
public enum Position
{
    Front,
    Middle,
    Back
}

public class Unit_Ablity
{
    public string Name;
    public string Element;
    public int AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public int Shild;
    public int maxHP;
    public string Team;
    public int GroupID;
    public UnitState State;
    public Position Position;

    protected UnitData Data;
    protected BuffModifier BuffModifiers;

    public Unit_Ablity(UnitData data)
    {
        Data = data;
        Name = data.Name;
        Element = data.Element;
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

    public void RecalculBuff(List<Buff_Base> buffs)
    {
        BuffModifiers = new BuffModifier();

        foreach (var buff in buffs)
        {
            BuffModifiers.AT += buff.Modifier.AT * buff.Level;
            BuffModifiers.DF += buff.Modifier.DF * buff.Level;
            BuffModifiers.SP += buff.Modifier.SP * buff.Level;
            BuffModifiers.HP += buff.Modifier.HP * buff.Level;
            BuffModifiers.CR += buff.Modifier.CR * buff.Level;
            BuffModifiers.CD += buff.Modifier.CD * buff.Level;
            BuffModifiers.RD += buff.Modifier.RD * buff.Level;
            BuffModifiers.ID += buff.Modifier.ID * buff.Level;

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

    public void OnDamaged(Unit target, float damage, DType dT, int ignore)
    {
        float dam = damage * 100 / (100 + DF * (1 - ignore / 100));
        target.Ui.ShowFloatingText(dam.ToString(), Color.red);

        switch (dT)
        {
            case DType.Normal:
                {
                    if (Shild > 0)
                    {
                        Shild -= (int)(dam * (1 - RD / 100));
                        if (Shild <= 0)
                        {
                            HP += Shild;
                            Shild = 0;
                        }
                        break;
                    }
                    else
                    {
                        HP -= (int)(dam * (1 - RD / 100));
                        break;
                    }         
                }
            case DType.Penetrate:
                {
                    HP -= (int)(dam * (1 - RD / 100));
                    break;
                }
            case DType.True: 
                {
                    HP -= (int)damage;
                    break;
                }
        }

        if (HP <= 0)
            HP = 0;          
    }

    public void OnHealed(Unit target, float heal)
    {
        target.Ui.ShowFloatingText(heal.ToString(), Color.green);

        if (heal + HP < maxHP)
            HP += (int)(heal);
        else
            HP = maxHP;
    }
    public void OnShieldGained(Unit target, float shild)
    {
        target.Ui.ShowFloatingText(shild.ToString(), Color.yellow);
        Shild += (int)(shild);
    }
}
