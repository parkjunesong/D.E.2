using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostFieldStatus { Balance, ManaHigh, PranaHigh, KarnaHigh, Regeneration, Null }

public class FieldEffectManager : MonoBehaviour
{
    public static FieldEffectManager Instance { get; private set; }
    public Buff_Base[] FieldEffects = new Buff_Base[5];
    public CostFieldStatus currentFieldStatus;
    private Buff_Base currentEffect;
    private int executeCount;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }
    public void TurnStart() // battleManager.BattleStart()에서 필드효과 Balance로 전투 시작
    {
        if (currentFieldStatus != CostFieldStatus.KarnaHigh && currentFieldStatus != CostFieldStatus.Regeneration)
        {
            CostFieldCheck();
            executeCount++;
            if (executeCount % 10 == 0) UpdateFieldEffect();
        }      
        else
        {
            if (currentFieldStatus == CostFieldStatus.KarnaHigh)
            {
                UpdateFieldEffect();
                FieldEffect_KarnaHigh();
            }
            if (currentFieldStatus == CostFieldStatus.Regeneration)
            {
                UpdateFieldEffect();
                FieldEffect_Regeneration();
            }
        }    
    }

    public void UpdateFieldEffect()
    {
        executeCount = 0;        

        // 이전 필드효과 삭제
        if(currentEffect != null)
        {
            foreach (Unit unit in UnitManager.Instance.alivePlayerUnits)
            {
                unit.Buff.RemoveByName(currentEffect.Name);
            }
            foreach (Unit unit in UnitManager.Instance.EnemyUnits)
            {
                unit.Buff.RemoveByName(currentEffect.Name);
            }
        }
        
        // 새로운 필드효과 적용
        switch (currentFieldStatus)
        {
            case CostFieldStatus.Balance:
                {
                    currentEffect = FieldEffects[0];
                    BattleManager.Instance.GetFreeRotation(1);                 
                    break;
                }
            case CostFieldStatus.ManaHigh:
                {
                    currentEffect = FieldEffects[1];
                    break;
                }
            case CostFieldStatus.PranaHigh:
                {
                    currentEffect = FieldEffects[2];
                    break;
                }
            case CostFieldStatus.KarnaHigh:
                {
                    currentEffect = FieldEffects[3];
                    break;
                }
            case CostFieldStatus.Regeneration:
                {
                    currentEffect = FieldEffects[4];
                    break;
                }
        }
        foreach (Unit unit in UnitManager.Instance.alivePlayerUnits)
        {
            unit.Buff.OnBuffGained(currentEffect, 1);
        }
        foreach (Unit unit in UnitManager.Instance.EnemyUnits)
        {
            unit.Buff.OnBuffGained(currentEffect, 1);
        }
    }
    
    public void CostFieldCheck() 
    {
        int[] CostNow = CostManager.Instance.CostCount();

        if (CostNow[2] >= CostNow[0] + CostNow[1] + CostNow[3] + CostNow[4] || 
            CostNow[0] >= CostManager.Instance.CostList.Count || 
            CostNow[1] >= CostManager.Instance.CostList.Count)
        {
            currentFieldStatus = CostFieldStatus.KarnaHigh;
        }
        else if (CostNow[0] > CostNow[1])
        {
            currentFieldStatus = CostFieldStatus.ManaHigh;
        }
        else if (CostNow[0] < CostNow[1])
        {
            currentFieldStatus = CostFieldStatus.PranaHigh;
        }
        else if (CostNow[0] == CostNow[1] && CostNow[2] + CostNow[3] + CostNow[4] == 0)
        {
            currentFieldStatus = CostFieldStatus.Balance;
        }
        else
            currentFieldStatus = CostFieldStatus.Null;
    }

    void FieldEffect_KarnaHigh()
    {
        int[] count = CostManager.Instance.CostCount();

        if (count[0] >= 1 && count[1] >= 1)
        {
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana), true);
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana), true);
        }
        else if (count[0] >= 2)
        {
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana), true);
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana), true);
        }
        else if (count[1] >= 2)
        {
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana), true);
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana), true);
        }
        else if (count[0] == 1)
        {
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana), true);
        }
        else if (count[1] == 1)
        {
            CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana), true);
        }

        if (count[2] == CostManager.Instance.CostList.Count)
        {
            currentFieldStatus = CostFieldStatus.Regeneration;
            UpdateFieldEffect();
        }              
    }
    void FieldEffect_Regeneration()
    {
        int[] count = CostManager.Instance.CostCount();
        if (count[2] >= 2)
        {
            CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna), true);
            CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna), true);
        }
        else if (count[2] == 1)
        {
            CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna), true);
        }

        if (count[2] + count[3] + count[4] == 0)
        {
            CostFieldCheck();
            UpdateFieldEffect();
        }
    }
}
