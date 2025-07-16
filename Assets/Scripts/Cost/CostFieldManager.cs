using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CostFieldStatus { Balance, ManaHigh, PranaHigh, KarnaHigh }

public class CostFieldManager : MonoBehaviour
{
    public static CostFieldManager Instance { get; private set; }
    public Buff_Base[] FieldEffects = new Buff_Base[4];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
    }
    public void UpdateFieldEffect()
    {
        switch (CostFieldCheck())
        {
            case CostFieldStatus.Balance:
                {
                    Debug.Log("### Field Effect Update ###");
                    Debug.Log("New Effect: " + CostFieldStatus.Balance);
                    Debug.Log("필드 위 모든 유닛의 받는 피해 10% 감소"); // 프리 로테이션 구현 바람

                    foreach (Unit unit in BattleManager.Instance.alivePlayerUnits)
                    {
                        unit.Buff.OnBuffGained(FieldEffects[0]);
                    }

                    foreach (Unit unit in BattleManager.Instance.EnemyUnits)
                    {
                        unit.Buff.OnBuffGained(FieldEffects[0]);
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

    public CostFieldStatus CostFieldCheck()
    {
        int[] CostNow = CostManager.Instance.CostCount();

        if (CostNow[2] >= CostNow[0] + CostNow[1])
        {
            return CostFieldStatus.KarnaHigh;
        }
        else
        {
            if (CostNow[0] == CostNow[1])
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
        return 0;
    }

}
