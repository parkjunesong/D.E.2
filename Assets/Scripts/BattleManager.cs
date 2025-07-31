using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.PlayerSettings;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }
   
    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();

    public List<Unit> alivePlayerUnits = new();
    public List<Unit> deadPlayerUnits = new();
    public Unit SelectedPlayerUnit, SelectedEnemyUnit;

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
        SelectedPlayerUnit = PlayerUnits[0];
        SelectedPlayerUnit.Ui.UpdateSelectIcon(true);
        SelectedEnemyUnit = EnemyUnits[0];
        SelectedEnemyUnit.Ui.UpdateSelectIcon(true);

        alivePlayerUnits = PlayerUnits;
                  
        FieldEffectManager.Instance.currentFieldStatus = CostFieldStatus.Balance;
        FieldEffectManager.Instance.UpdateFieldEffect();
        TurnStart();
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        gameObject.GetComponent<SkillManager>().uiReset();

        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnStart();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnStart();
        }
        FieldEffectManager.Instance.TurnStart();
    }
    public void TurnEnd()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnEnd();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnEnd();
        }
        SkillManager.Instance.TurnEnd();
        TurnStart();
    }
    public void Rotation()
    {
        if (alivePlayerUnits.Count <= 1) return;

        var tmp = alivePlayerUnits[0];
        alivePlayerUnits.RemoveAt(0);
        alivePlayerUnits.Insert(alivePlayerUnits.Count, tmp);

        for (int i = 0; i < alivePlayerUnits.Count; i++)
        {
            alivePlayerUnits[i].GetComponent<PlayerUnit>().Position = (Position)i;
            alivePlayerUnits[i].transform.position = GetPositionPlayer((Position)i);
            alivePlayerUnits[i].Ui.UiRotation((Position)i);
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

    public void OnUnitDied(Unit unit)
    {      
        if (unit.Ability.Team == "Player")
        {
            // ±âÁ¸ »ç¸ÁÀ¯´Ö ÀçÁ¤·Ä          
            for (int i = 0; i < deadPlayerUnits.Count && i < 1; i++)
            {
                Position pos = (Position)(1 - i);
                deadPlayerUnits[i].GetComponent<PlayerUnit>().Position = pos;
                deadPlayerUnits[i].transform.position = GetPositionPlayer(pos);
            }

            // Áö±Ý Á×Àº À¯´Ö Ã³¸®
            unit.Ability.State = UnitState.Dead;
            unit.GetComponent<PlayerUnit>().Position = Position.Back;
            unit.transform.position = GetPositionPlayer(Position.Back);
            deadPlayerUnits.Add(unit);

            // »ì¾ÆÀÖ´Â À¯´Ö ÀçÁ¤·Ä
            alivePlayerUnits = PlayerUnits.Where(u => u.Ability.State == UnitState.Alive).OrderBy(u => u.GetComponent<PlayerUnit>().Position).ToList();
            for (int i = 0; i < alivePlayerUnits.Count; i++)
            {
                alivePlayerUnits[i].GetComponent<PlayerUnit>().Position = (Position)i;
                alivePlayerUnits[i].transform.position = GetPositionPlayer((Position)i);
            }
            if (SelectedPlayerUnit == unit)
            {
                unit.Ui.UpdateSelectIcon(false);
                SelectedPlayerUnit = alivePlayerUnits[0];
                SelectedPlayerUnit.Ui.UpdateSelectIcon(true);
            }
        }
        else if (unit.Ability.Team == "Enemy")
        {
            EnemyUnits.Remove(unit);
            if (SelectedEnemyUnit == unit)
            {
                unit.Ui.UpdateSelectIcon(false);
                SelectedEnemyUnit = EnemyUnits[0];
                SelectedEnemyUnit.Ui.UpdateSelectIcon(true);
            }           
            Destroy(unit.gameObject);

            for (int i = 0; i < EnemyUnits.Count; i++)
            {
                EnemyUnits[i].transform.position = GetPositionEnemy(i);
            }           
        }
    }

    public Vector2 GetPositionPlayer(Position pos)
    {
        return pos switch
        {
            Position.Front => new Vector2(-400, 1000),
            Position.Middle => new Vector2(-600, 1000),
            Position.Back => new Vector2(-800, 1000),
            _ => new Vector2(0, 0)
        };
    }
    public Vector2 GetPositionEnemy(int i)
    {
        return i switch
        {
            0 => new Vector2(200, 1000),
            1 => new Vector2(400, 1000),
            2 => new Vector2(600, 1000),
            3 => new Vector2(800, 1000),
            4 => new Vector2(1000, 1000),
            5 => new Vector2(1200, 1000),
            6 => new Vector2(1400, 1000),
            _ => new Vector2(0, 0)
        };
    }
}