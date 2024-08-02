using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Ablity : Unit_Base
{
    public int AT, SP, HP, DF;
    public float CR, CD, DR;
    public string Element;

    public override void Damaged(int damage)
    {
        Debug.Log("Damaged: " + damage);
    }
}
