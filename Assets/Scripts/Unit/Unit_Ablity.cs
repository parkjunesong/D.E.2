using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

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
    }

    public void RecalculBuff(List<Buff_Base> buffs)
    {
        BuffModifiers = new BuffModifier();

        foreach (var buff in buffs)
        {
            BuffModifiers.AT += buff.Modifier.AT * buff.currentStack;
            BuffModifiers.DF += buff.Modifier.DF * buff.currentStack;
            BuffModifiers.SP += buff.Modifier.SP * buff.currentStack;
            BuffModifiers.HP += buff.Modifier.HP * buff.currentStack;
            BuffModifiers.CR += buff.Modifier.CR * buff.currentStack;
            BuffModifiers.CD += buff.Modifier.CD * buff.currentStack;
            BuffModifiers.RD += buff.Modifier.RD * buff.currentStack;
            BuffModifiers.ID += buff.Modifier.ID * buff.currentStack;

            AT = (int)(Data.AT * (1 + BuffModifiers.AT));
            SP = (int)(Data.SP * (1 + BuffModifiers.SP));
            maxHP = (int)(Data.HP * (1 + BuffModifiers.HP));
            DF = (int)(Data.DF * (1 + BuffModifiers.DF));
            CR = Data.CR + BuffModifiers.CR;
            CD = Data.CD + BuffModifiers.CD;
            RD = Data.RD + BuffModifiers.RD;
            ID = Data.ID + BuffModifiers.ID;
        }
    }

    public void OnDamaged(float damage, DType dT, int ignore)
    {
        float dam = damage * 100 / (100 + DF * (1 - ignore / 100));

        switch (dT)
        {
            case DType.Normal:
                {
                    Debug.Log("damage: " + dam * (1 - RD / 100));
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

        if (HP <= 0) HP = 0;
    }

    public void OnHealed(float heal)
    {       
        if (heal + HP < maxHP)
            HP += (int)(heal);
        else
            HP = maxHP;
    }
    public void OnShieldGained(float shild)
    {
        Shild += (int)(shild);
    }
}
