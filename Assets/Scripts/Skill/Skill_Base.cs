using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Skill_Base : MonoBehaviour
{
    public int[] Skill_Cost;
    public int Skill_CoolTime;
    public string Skill_Name;
    public Sprite Skill_Icon;
    public Effect_Base[] Effects;
    
    public abstract void execute(Unit_Ablity ability);
    public virtual void TurnStart() { }
    public virtual void TurnEnd() { }
}
