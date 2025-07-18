using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Passive_CronoaAlpha : Unit_Passive
{
    public override void OnTurnStart()
    {
        PassiveStack += 1;
        if(Unit.GetComponent<CharaUnit>().Position == Position.Front)
            PassiveStack += 1;
        if (PassiveStack > MaxStack)
        {
            int overflow = PassiveStack - MaxStack;
            PassiveStack = MaxStack;

            foreach (var unit in BattleManager.Instance.alivePlayerUnits)
                foreach (var skill in unit.Skill.SkillList)
                {
                    skill.ReduceCoolTime(overflow);
                }                   
        }
    } 
}