using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_Test : Skill_Base
{
    void Awake()
    {
        SEM = GameObject.Find("SkillEffectManager");

        Effects[0] = SEM.GetComponent<Effect_Damage>();
    }
    public override void execute()
    {
        Debug.Log("use skill");
        Effects[0].execute();
        
    }

}
