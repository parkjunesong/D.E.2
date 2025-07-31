using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_Iris : Unit_Passive
{
    public override void OnActionExecuted()
    {
        PassiveStack++;
        if (PassiveStack > MaxStack)
        {
            PassiveStack = 0;
            Execute();
        }
    }
}