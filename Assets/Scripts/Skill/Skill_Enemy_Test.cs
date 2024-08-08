using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class Skill_Enemy_Test : Skill_Base
{
    protected int MoveCount;
    public override void Awake()
    {
        base.Awake();
        MoveCount = Skill_CoolTime;
    }
   
    public override void execute(Unit_Ablity ability)
    {
        Effects[0].execute(ability);
    }
    public override void TurnStart()
    {
        MoveCount--;
        resetUi();
    }
    public override void TurnEnd()
    {
        if (MoveCount == 0)
        {
            Unit_Ablity unit = transform.parent.parent.GetComponent<Unit_Ablity>();
            execute(unit);
            MoveCount = Skill_CoolTime;
        }
    }

    void resetUi()
    {
        CountUi.text = MoveCount + "";
        if (MoveCount == 0)
            CountUi.color = new Color32(255, 0, 0, 255);
        else if (CountUi.color == new Color32(255, 0, 0, 255))
            CountUi.color = new Color32(0, 0, 0, 255);
    }
}