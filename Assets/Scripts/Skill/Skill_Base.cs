using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill_Base : MonoBehaviour
{
    public int[] Skill_Cost;
    public string Skill_Name;
    public Sprite Skill_Icon;
    protected GameObject SEM;
    protected Effect_Base[] Effects = new Effect_Base[10];
    public abstract void execute();
}
