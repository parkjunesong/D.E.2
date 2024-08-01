using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Damage : Effect_Base
{
    enum DamageType { Normal, Penetrate, True };
    enum AttackType { };
    public override void execute()
    {
        Debug.Log("Attack");
    }
}
