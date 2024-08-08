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
    protected Text CountUi;
    BattleSystem bs;

    public virtual void Awake()
    {
        bs = GameObject.Find("GameManager").GetComponent<BattleSystem>();
        CountUi = transform.parent.parent.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
    }
    public abstract void execute(Unit_Ablity ability);
    public void AfterUseSkill()
    {
        bs.TurnEnd();
    }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() { }
}
