using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Base : MonoBehaviour
{
    public int[] Skill_Cost;
    public string Skill_Name;
    public Sprite Skill_Icon;
    protected GameObject SEM;
    protected Effect_Base[] Effects;
    public abstract void execute();

    protected virtual void Awake()
    {
        SEM = GameObject.Find("SkillEffectManager"); // 자식에서 한번씩 중복으로 실행되는중
    }
}
