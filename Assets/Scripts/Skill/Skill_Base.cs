using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Base : MonoBehaviour
{
    public int[] Skill_Cost;
    public string Skill_Name;
    public Sprite Skill_Icon;
    public Effect_Base[] Effects;
    public abstract void execute(Unit_Ablity ability);
    public void AfterUseSkill()
    {
        GameObject.Find("GameManager").GetComponent<BattleSystem>().TurnEnd();
    }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() { }
}
