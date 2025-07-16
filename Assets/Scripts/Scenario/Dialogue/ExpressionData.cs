using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object/Scenario/Character")]
public class ExpressionData : ScriptableObject
{
    public string Id;
    public string Name;
    public List<Expression> expressions;

    [System.Serializable]
    public class Expression
    {
        public string name;
        public Sprite sprite;
    }

    public Sprite GetExpression(string expressionName)
    {
        var exp = expressions.Find(e => e.name == expressionName);
        return exp != null ? exp.sprite : null;
    }
}