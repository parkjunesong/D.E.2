using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UnitType { Unit_Alive, Unit_Dead, Summoned, Object }
[CreateAssetMenu(fileName = "UnitData", menuName = "Scriptable Object/UnitData", order = int.MaxValue)]
public class UnitData : ScriptableObject
{
    public string Name;
    public string Element;
    public UnitType UT;
    public int AT, SP, HP, DF;
    public float CR, CD;
    public float RD, ID; // ReduceDamage, IncreaseDamage 
    public Sprite Face, Standing;
}
