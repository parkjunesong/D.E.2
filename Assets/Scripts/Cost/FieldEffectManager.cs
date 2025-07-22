using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum CostFieldStatus { Balance, ManaHigh, PranaHigh, KarnaHigh, Regeneration, Null }

public class FieldEffectManager : MonoBehaviour
{
    public static FieldEffectManager Instance { get; private set; }
    public Buff_Base[] FieldEffects = new Buff_Base[5];
    public Buff_Base currentEffect;
    bool isRegeneration;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;
        currentEffect = null;
        isRegeneration = false;
    }
    public void TurnStart()
    {
        if (BattleManager.Instance.Turn % 10 == 0) // 10배수 턴마다 FieldEffect 갱신
        {
            UpdateFieldEffect();
        }
        if (currentEffect == null) return;

        if (currentEffect.Name == "KarnaHigh")
        {
            int[] count = CostManager.Instance.CostCount();
            if (count[0] >= 1 && count[1] >= 1)
            {
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana));
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana));
            }  
            else if (count[0] >= 2)
            {
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana));
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana));
            }
            else if (count[1] >= 2)
            {
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana));
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana));
            }
            else if (count[0] == 1)
            {
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Mana));
                isRegeneration = true;
                UpdateFieldEffect();
            }
            else if (count[1] == 1)
            {
                CostManager.Instance.Cost_Vanish(CostManager.Instance.CostFindNum(CostType.Prana));
                isRegeneration = true;
                UpdateFieldEffect();
            }
            else
            {
                isRegeneration = true; 
                UpdateFieldEffect();
            }
        }
        else if (isRegeneration)
        {
            int[] count = CostManager.Instance.CostCount();
            if (count[2] >= 2)
            {
                CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna));
                CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna));
            }
            else if (count[2] == 1)
            {
                CostManager.Instance.Cost_Regeneration(CostManager.Instance.CostFindNum(CostType.Karna));
                isRegeneration = false;
                UpdateFieldEffect();
            }
            else
            {
                isRegeneration = false;
                UpdateFieldEffect();
            }
        }        
    }

    public void UpdateFieldEffect()
    {
        RemoveFieldEffect();
        Debug.Log(CostFieldCheck());
        switch (CostFieldCheck())
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
        foreach (Unit unit in BattleManager.Instance.alivePlayerUnits)
        {
            unit.Buff.OnBuffGained(currentEffect, 1);
        }
        foreach (Unit unit in BattleManager.Instance.EnemyUnits)
        {
            unit.Buff.OnBuffGained(currentEffect, 1);
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

        if (isRegeneration)
        {
            return CostFieldStatus.Regeneration;
        }
        if (CostNow[2] >= CostNow[0] + CostNow[1] || CostNow[0] >= 12 || CostNow[1] >= 12)
        {
            return CostFieldStatus.KarnaHigh;
        }
        if (CostNow[0] == CostNow[1] && CostNow[2] + CostNow[3] == 0)
        {
            return CostFieldStatus.Balance;
        }
        if (CostNow[0] > CostNow[1])
        {
            return CostFieldStatus.ManaHigh;
        }
        if (CostNow[0] < CostNow[1])
        {
            return CostFieldStatus.PranaHigh;
        }
        return CostFieldStatus.Null;
    }
}
