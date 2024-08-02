using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test : Skill_Base
{
    protected override void Awake()
    {
        base.Awake();
        Effects = new Effect_Base[1];
        Effects[0] = SEM.GetComponent<Effect_Damage>();
    }
    public override void execute()
    {
        Effects[0].execute();
    }
    
}
