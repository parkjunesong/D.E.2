using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public abstract class Effect_Base : ScriptableObject
{
    public AType AimType;
    public float Value;

    public abstract void Execute(Unit caster, List<Unit> targets);
}
