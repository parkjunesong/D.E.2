using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

public class Skill_Test : Skill_Base
{
    public override void execute(Unit_Ablity ability)
    {
        Effects[0].execute(ability);
        SystemManager.system.TurnEnd();
    }
    
}
