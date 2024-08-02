using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit: Unit_Base
{
    public string Unit_Name;
    public Unit_Ablity Ability;
    public Unit_Animation Animation;
    void Awake()
    {
        Ability = gameObject.GetComponent<Unit_Ablity>();
        Animation = gameObject.GetComponent<Unit_Animation>();
    }
}
