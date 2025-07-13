using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnit : Unit
{
    int skillNo;
    public int MoveCount;
    public override void Init()
    {
        base.Init();

        skillNo = 0;
        MoveCount = Skill.Skills[skillNo].Skill_CoolTime; // 패턴 따라 스킬 교체
    }

    public override void TurnStart()
    {
        if (MoveCount == 0)
        {
            OnSkillUsed(skillNo);
            MoveCount = Skill.Skills[0].Skill_CoolTime;
        }
    }
    public override void TurnEnd()
    {
        MoveCount--;
    }
}