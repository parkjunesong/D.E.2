using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Passive_CronoaAlpha : Unit_Passive
{
    public override void OnTurnStart()
    {
        PassiveStack += 1;
        if(Unit.GetComponent<CharaUnit>().Position == Position.Front)
            PassiveStack += 1;
        if (PassiveStack > MaxStack)
        {
            PassiveStack = PassiveStack - MaxStack;
            Execute();
            PassiveStack = MaxStack;
        }
    }   
}