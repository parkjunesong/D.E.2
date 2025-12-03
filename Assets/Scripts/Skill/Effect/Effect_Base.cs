using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public enum AType { Select, Front, Back, Random, Self };
public enum ATarget { Player, Enemy };

public abstract class Effect_Base
{
    public float Value;
    public int Range;   
    public AType AimType;
    public ATarget AimTarget;

    public Effect_Base(float value, int range, AType aimType, ATarget aimTarget)
    {
        Value = value;
        Range = range;
        AimType = aimType;
        AimTarget = aimTarget;
    }
    public virtual void Execute(Unit caster) { }
    public virtual void Execute(Unit caster, float CoE, int[] cost) { }

    public List<Unit> setTarget(Unit caster)
    {
        var UM = UnitManager.Instance;
        if (AimTarget == ATarget.Enemy)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(UM.SelectedEnemyUnit, Range, UM.EnemyUnits);
                case AType.Front:
                    return GetNearUnits(UM.EnemyUnits[0], Range, UM.EnemyUnits);
                case AType.Back:
                    return GetNearUnits(UM.EnemyUnits[UM.EnemyUnits.Count - 1], Range, UM.EnemyUnits);
                case AType.Random:
                    return GetNearUnits(UM.EnemyUnits[Random.Range(0, UM.EnemyUnits.Count)], Range, UM.EnemyUnits);
                case AType.Self:
                    return new List<Unit>() { caster };
            }
        }
        else if (AimTarget == ATarget.Player)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(UM.SelectedPlayerUnit, Range, UM.alivePlayerUnits);
                case AType.Front:
                    return GetNearUnits(UM.alivePlayerUnits[0], Range, UM.alivePlayerUnits);
                case AType.Back:
                    return GetNearUnits(UM.alivePlayerUnits[UM.alivePlayerUnits.Count - 1], Range, UM.alivePlayerUnits);
                case AType.Random:
                    return GetNearUnits(UM.alivePlayerUnits[Random.Range(0, UM.alivePlayerUnits.Count)], Range, UM.alivePlayerUnits);
                case AType.Self:
                    return new List<Unit>() { caster };
            }
        }
        return null;
    }
    public List<Unit> GetNearUnits(Unit middle, int range, List<Unit> group)
    {
        List<Unit> nearby = new List<Unit>();

        if (range == 0)
            nearby.Add(middle);
        else
        {
            for (int i = -range; i <= range; i++)
            {
                int targetIndex = group.IndexOf(middle) + i;

                if (targetIndex >= 0 && targetIndex < group.Count)
                    nearby.Add(group[targetIndex]);
            }
        }
        return nearby;
    }
    public float GetFieldEffectCoE(int[] cost)
    {
        int value = 0;
        if (FieldEffectManager.Instance.currentFieldStatus == CostFieldStatus.Null) return 0;
        switch (FieldEffectManager.Instance.currentFieldStatus)
        {
            case CostFieldStatus.ManaHigh:
                {
                    value += cost[0] * 3;
                    break;
                }
            case CostFieldStatus.PranaHigh:
                {
                    value += cost[1] * 3;
                    break;
                }
            case CostFieldStatus.KarnaHigh:
                {
                    if (cost[2] != 0)
                    {
                        value += cost[2] * 3;
                    }
                    else
                    {
                        value = -10;
                    }
                    break;
                }
        }
        return value;
    }
}
