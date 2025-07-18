using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostFieldStatus { Balance, ManaHigh, PranaHigh, KarnaHigh, Null}

public class CostFieldManager : MonoBehaviour
{
    public static CostFieldManager Instance { get; private set; }
    public Buff_Base[] FieldEffects = new Buff_Base[4];
    private Buff_Base currentEffect;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;
        currentEffect = null;
    }

    public void UpdateFieldEffect()
    {
        RemoveFieldEffect();

        switch (CostFieldCheck())
        {
            case CostFieldStatus.Balance:
                {
                    currentEffect = FieldEffects[0];
                    BattleManager.Instance.GetFreeRotation(1);

                    foreach (Unit unit in BattleManager.Instance.alivePlayerUnits)
                    {
                        unit.Buff.OnBuffGained(currentEffect, 1);
                    }
                    foreach (Unit unit in BattleManager.Instance.EnemyUnits)
                    {
                        unit.Buff.OnBuffGained(currentEffect, 1);
                    }
                    break;
                }
            case CostFieldStatus.ManaHigh:
                {
                    break;
                }
            case CostFieldStatus.PranaHigh:
                {
                    break;
                }
            case CostFieldStatus.KarnaHigh:
                {
                    break;
                }
        }
    }
    public void RemoveFieldEffect()
    {
        if (currentEffect == null) return;

        foreach (Unit unit in BattleManager.Instance.alivePlayerUnits)
        {
            unit.Buff.RemoveByName(currentEffect.Name); 
        }
        foreach (Unit unit in BattleManager.Instance.EnemyUnits)
        {
            unit.Buff.RemoveByName(currentEffect.Name); 
        }
    }


    public CostFieldStatus CostFieldCheck()
    {
        int[] CostNow = CostManager.Instance.CostCount();

        if (CostNow[2] >= CostNow[0] + CostNow[1])
        {
            return CostFieldStatus.KarnaHigh;
        }
        else
        {
            if (CostNow[0] == CostNow[1] && CostNow[2] + CostNow[3] == 0)
            {

                return CostFieldStatus.Balance;               
            }
            else if (CostNow[0] > CostNow[1])
            {
                return CostFieldStatus.ManaHigh;
            }
            else if (CostNow[0] < CostNow[1])
            {
                return CostFieldStatus.PranaHigh;
            }
        }
        return CostFieldStatus.Null;
    }

}
