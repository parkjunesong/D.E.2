using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public GameObject origin;
    public List<BattleUnit> PlayerUnits = new();
    public List<BattleUnit> EnemyUnits = new();
    public Unit SelectedPlayerUnit, SelectedEnemyUnit;

    public int Turn;
    GameObject TurnUi;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
    }

    public void BattleStart()
    {
        Turn = 0;
        TurnUi = GameObject.Find("Turn");
        SelectedPlayerUnit = PlayerUnits[0].Unit;
        SelectedPlayerUnit.Ui.UpdateSelectIcon(true);
        SelectedEnemyUnit = EnemyUnits[0].Unit;
        SelectedEnemyUnit.Ui.UpdateSelectIcon(true);
        TurnStart();
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        gameObject.GetComponent<SkillManager>().uiReset();

        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].Unit.TurnStart();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].Unit.TurnStart();
        }
    }
    public void TurnEnd()
    {
        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].Unit.TurnEnd();
        }
        for (int i = 0; i < EnemyUnits.Count; i++)
        {
            EnemyUnits[i].Unit.TurnEnd();
        }
        TurnStart();
    }
    public void Rotation()
    {
        Vector2[] positions = {
            new Vector2(-200, 1100), // Front
            new Vector2(-480, 1100),    // Middle
            new Vector2(-760, 1100)   // Back
        };

        if (PlayerUnits.Count == 0) return;

        var last = PlayerUnits[PlayerUnits.Count - 1];
        PlayerUnits.RemoveAt(PlayerUnits.Count - 1);
        PlayerUnits.Insert(0, last);

        for (int i = 0; i < PlayerUnits.Count; i++)
        {
            PlayerUnits[i].Position = (Position)i;
            PlayerUnits[i].GameObject.transform.position = positions[(int)(Position)i];
        }
        CostManager.cost.CostReset();
        TurnEnd();
    }

    public void UnitSpawn(UnitData data, Vector2 xy, string Team, int GroupID)
    {
        BattleUnit battleUnit;
        GameObject Unit = Instantiate(origin, new Vector2(xy.x, xy.y), Quaternion.identity);

        if (Team == "Player")
            Unit.AddComponent<CharaUnit>();
        else if (Team == "Enemy")
            Unit.AddComponent<EnemyUnit>();

        Unit.GetComponent<Unit>().Data = data;
        Unit.GetComponent<Unit>().Init();
        Unit.GetComponent<Unit>().Ability.Team = Team;
        Unit.GetComponent<Unit>().Ability.GroupID = GroupID;
        Unit.name = Team + GroupID;
        battleUnit = new BattleUnit(Unit);
        
        if (Team == "Player")
            PlayerUnits.Add(battleUnit);
        else if (Team == "Enemy")
            EnemyUnits.Add(battleUnit);        
    }
    
    public void ClearDeadUnits()
    {
        PlayerUnits.RemoveAll(u => !u.IsAlive);
        EnemyUnits.RemoveAll(u => !u.IsAlive);
    }

    public bool IsPlayerUnit(Unit unit)
    {
        return PlayerUnits.Any(bu => bu.Unit == unit);
    }
    public bool IsEnemyUnit(Unit unit)
    {
        return EnemyUnits.Any(bu => bu.Unit == unit);
    }
    public BattleUnit GetBattleUnit(Unit unit)
    {
        if (IsPlayerUnit(unit))
            return PlayerUnits.FirstOrDefault(bu => bu.Unit == unit);
        if (IsEnemyUnit(unit))
            return EnemyUnits.FirstOrDefault(bu => bu.Unit == unit);
        return null;
    }

}