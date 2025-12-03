using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public int Turn;
    GameObject TurnUi;
    GameObject RotationUi;
    int FreeRotateCount;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void BattleStart()
    {
        Turn = 0;
        FreeRotateCount = 0;
        TurnUi = GameObject.Find("Turn");
        RotationUi = GameObject.Find("rotation");
                          
        FieldEffectManager.Instance.currentFieldStatus = CostFieldStatus.Balance;
        FieldEffectManager.Instance.UpdateFieldEffect();
        UnitManager.Instance.init();
        TurnStart();
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        gameObject.GetComponent<SkillManager>().uiReset();

        UnitManager.Instance.TurnStart();
        FieldEffectManager.Instance.TurnStart();
    }
    public void TurnEnd()
    {
        UnitManager.Instance.TurnEnd();
        SkillManager.Instance.TurnEnd();
        TurnStart();
    }
    public void Rotation()
    {
        var units = UnitManager.Instance.alivePlayerUnits;
        if (units.Count <= 1) return;

        var tmp = units[0];
        units.RemoveAt(0);
        units.Insert(units.Count, tmp);

        for (int i = 0; i < units.Count; i++)
        {
            units[i].GetComponent<PlayerUnit>().Position = (Position)i;
            units[i].transform.position = UnitManager.Instance.GetPositionPlayer((Position)i);
            units[i].Ui.UiRotation((Position)i);
        }

        CostManager.Instance.CostReset();
        if (FreeRotateCount > 0)
        {
            FreeRotateCount--;
            if (FreeRotateCount > 0)
                RotationUi.transform.GetChild(0).GetComponent<Text>().text = FreeRotateCount.ToString();
            else
                RotationUi.transform.GetChild(0).GetComponent<Text>().text = "";
        }
        else
            TurnEnd();
    }
    public void GetFreeRotation(int count)
    {
        FreeRotateCount += count;
        RotationUi.transform.GetChild(0).GetComponent<Text>().text = FreeRotateCount.ToString();
    }
}