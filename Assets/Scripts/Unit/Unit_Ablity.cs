using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Unit_Ablity
{
    public string Team;
    public string Name;
    public string Element;
    public UnitType UT;
    public int AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 

    public Unit_Ablity(UnitData ablity)
    {
        Name = ablity.Name;
        Element = ablity.Element;
        UT = ablity.UT;
        AT = ablity.AT;
        SP = ablity.SP;
        HP = ablity.HP;
        DF = ablity.DF;
        CR = ablity.CR;
        CD = ablity.CD;
        RD = ablity.RD;
        ID = ablity.ID;
    }


    public void Damaged(float damage, DType dT, int ignore)
    {
        float dam = damage * 100 / (100 + DF * (1 - ignore / 100));

        switch (dT)
        {
            case DType.Normal:
                {
                    Debug.Log("damage: " + dam * (1 - RD / 100));
                    HP -= (int)(dam * (1 - RD / 100));
                    break;
                }
            case DType.Penetrate: // 실드 미구현
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
}
