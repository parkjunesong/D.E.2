using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Enemy_Test : Skill_Base
{
    protected int MoveCount;
    void Awake()
    {
        MoveCount = Skill_CoolTime;
    }
   
    public override void execute(Unit_Ablity ability)
    {
        Effects[0].execute(ability);
    }
    public override void TurnStart()
    {
        MoveCount--;
        //resetUi();
    }
    public override void TurnEnd()
    {
        if (MoveCount == 0)
        {
            Unit_Ablity unit = transform.parent.parent.GetComponent<Unit>().Ability;
            execute(unit);
            MoveCount = Skill_CoolTime;
        }
    }
}