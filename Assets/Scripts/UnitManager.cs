using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    public static UnitManager Instance { get; private set; }

    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();
    public List<Unit> alivePlayerUnits = new();
    public List<Unit> deadPlayerUnits = new();
    public Unit SelectedPlayerUnit, SelectedEnemyUnit;

    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject EnemyPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void init()
    {
        SelectedPlayerUnit = PlayerUnits[0];
        SelectedPlayerUnit.Ui.UpdateSelectIcon(true);
        SelectedEnemyUnit = EnemyUnits[0];
        SelectedEnemyUnit.Ui.UpdateSelectIcon(true);
        alivePlayerUnits = PlayerUnits;
    }
    public void TurnStart()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].TurnStart();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].TurnStart();
        }
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

    public void Spawn(UnitData data, int count, string team)
    {
        if (team == "Player")
        {
            GameObject UnitGameObject = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);

            UnitGameObject.transform.position = GetPositionPlayer((Position)count);
            Unit unit = UnitGameObject.GetComponent<Unit>();
            unit.Data = Instantiate(data);
            unit.Init();
            unit.Ability.Team = team;
            unit.name = unit.Ability.Name;
            unit.GetComponent<PlayerUnit>().Position = (Position)count;
            unit.Ui.PlayerSet(unit.GetComponent<PlayerUnit>().Position, data.Face);
            unit.Ui.setElementIcon(unit.Ability.Elements);

            PlayerUnits.Add(unit);
        }
        else if (team == "Enemy")
        {
            GameObject UnitGameObject = Instantiate(EnemyPrefab, new Vector2(0, 0), Quaternion.identity);

            UnitGameObject.transform.position = GetPositionEnemy(count);
            Unit unit = UnitGameObject.GetComponent<Unit>();
            unit.Data = Instantiate(data);
            unit.Init();
            unit.Ability.Team = team;
            unit.name = unit.Ability.Name;
            unit.Ui.EnemySet();
            unit.Ui.setElementIcon(unit.Ability.Elements);

            EnemyUnits.Add(unit);
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
