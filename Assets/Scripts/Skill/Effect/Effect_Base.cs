using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public abstract class Effect_Base : ScriptableObject
{
    public AType AimType;
    public ATarget AimTarget;

    public abstract void Execute(Unit caster, List<Unit> targets);
}
