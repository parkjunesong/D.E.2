using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public enum AType { Select, Front, Back, Random, Self };
public enum ATarget { Player, Enemy };

public abstract class Effect_Base : ScriptableObject
{
    public int Range;
    public AType AimType;
    public ATarget AimTarget;

    public abstract void Execute(Unit caster, List<Unit> targets);
}
