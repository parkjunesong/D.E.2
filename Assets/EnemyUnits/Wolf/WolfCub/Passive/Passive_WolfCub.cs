using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Passive_WolfCub : Unit_Passive
{
    public override void OnDied()
    {
        Execute();
    }   
}