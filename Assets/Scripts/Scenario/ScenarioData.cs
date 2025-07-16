using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScenarioType { OnlyText, OnlyBattle, BattleFirst, BattleLast };

[CreateAssetMenu(fileName = "ScenarioData", menuName = "Scriptable Object/Scenario/ScenarioData", order = int.MaxValue)]
public class ScenarioData : ScriptableObject
{
    public ScenarioType Type;
    public string Name;
    public string Description;
    public DialogueData Dialogue;
    public MapData mData;
}