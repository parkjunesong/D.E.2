using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    public ExpressionData character;
    public string expression;
    public string text;
}

[CreateAssetMenu(menuName = "Scriptable Object/Scenario/Dialogue")]
public class DialogueData : ScriptableObject
{
    public DialogueLine[] lines;
}