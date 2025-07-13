using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ScenarioData", menuName = "Scriptable Object/ScenarioData", order = int.MaxValue)]
public class ScenarioData : ScriptableObject
{
    public string ScenarioName;
    public string Description;
    public TextAsset DialogueJSON;
    public MapData MapToLoad;
}