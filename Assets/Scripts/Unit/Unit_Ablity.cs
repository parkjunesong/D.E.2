using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Unit_Ablity : Unit_Base
{
    public int AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public string Element;

    public override void Damaged(float damage, DType dT, int ignore)
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
