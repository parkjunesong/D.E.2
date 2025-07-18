using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Element { Fate, Order, Creation, Ruin, Chaos, Void};

[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public string Name;
    public Element Element;
    public int AT, SP, HP, DF; // AT%, HP%, DF%, SP´Â ±ø½ºÅÝ
    public float CR, CD; // CritRate, 
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public Sprite Face, Standing, Dead;
    public List<Skill_Base> Skills;
    public GameObject Passive;
}
