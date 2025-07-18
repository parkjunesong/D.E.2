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
    public abstract void Execute(Unit caster);

    public List<Unit> setTarget(Unit caster)
    {
        BattleManager BM = BattleManager.Instance;
        if (AimTarget == ATarget.Enemy)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(BM.SelectedEnemyUnit, Range, BM.EnemyUnits);
                case AType.Front:
                    return GetNearUnits(BM.EnemyUnits[0], Range, BM.EnemyUnits);
                case AType.Back:
                    return GetNearUnits(BM.EnemyUnits[BM.EnemyUnits.Count - 1], Range, BM.EnemyUnits);
                case AType.Random:
                    return GetNearUnits(BM.EnemyUnits[Random.Range(0, BM.EnemyUnits.Count)], Range, BM.EnemyUnits);
                case AType.Self:
                    return new List<Unit>() { caster };
            }
        }
        else if (AimTarget == ATarget.Player)
        {
            switch (AimType)
            {
                case AType.Select:
                    return GetNearUnits(BM.SelectedPlayerUnit, Range, BM.alivePlayerUnits);
                case AType.Front:
                    return GetNearUnits(BM.alivePlayerUnits[0], Range, BM.alivePlayerUnits);
                case AType.Back:
                    return GetNearUnits(BM.alivePlayerUnits[BM.alivePlayerUnits.Count - 1], Range, BM.alivePlayerUnits);
                case AType.Random:
                    return GetNearUnits(BM.alivePlayerUnits[Random.Range(0, BM.alivePlayerUnits.Count)], Range, BM.alivePlayerUnits);
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
}
