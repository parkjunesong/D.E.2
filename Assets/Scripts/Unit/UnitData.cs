using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public string Name;
    public List<Element> Elements;
    public int AT, SP, HP, DF; // AT%, HP%, DF%, SP´Â ±ø½ºÅÝ
    public float CR, CD; // CritRate, 
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public Sprite Face, Standing, Dead;
    public List<Skill_Base> Skills;
    public Skill_Passive Passive;
}
