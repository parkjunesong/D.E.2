using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class BattleManager : MonoBehaviour
{
    public static BattleManager Instance { get; private set; }

    public GameObject origin;
    public List<Unit> PlayerUnits = new();
    public List<Unit> EnemyUnits = new();

    public List<Unit> alivePlayerUnits = new();
    public List<Unit> deadPlayerUnits = new();
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
        SelectedPlayerUnit = PlayerUnits[0];
        SelectedPlayerUnit.Ui.UpdateSelectIcon(true);
        SelectedEnemyUnit = EnemyUnits[0];
        SelectedEnemyUnit.Ui.UpdateSelectIcon(true);

        alivePlayerUnits = PlayerUnits;
        for (int i = 0; i < alivePlayerUnits.Count; i++)
            alivePlayerUnits[i].Ability.Position = (Position)i;


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
        if (Turn % 5 == 0) // 5배수 턴마다 FieldEffect 갱신
        {
            CostFieldManager.Instance.UpdateFieldEffect();
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
        TurnStart();
    }
    public void Rotation()
    {
        if (alivePlayerUnits.Count <= 1) return;

        var last = alivePlayerUnits[alivePlayerUnits.Count - 1];
        alivePlayerUnits.RemoveAt(alivePlayerUnits.Count - 1);
        alivePlayerUnits.Insert(0, last);

        for (int i = 0; i < alivePlayerUnits.Count; i++)
        {
            alivePlayerUnits[i].Ability.Position = (Position)i;
            alivePlayerUnits[i].transform.position = GetPositionVector((Position)i);
        }

        CostManager.Instance.CostReset();
        TurnEnd();
    }

    public void UnitSpawn(UnitData data, Vector2 xy, string Team, int GroupID)
    {
        GameObject UnitGameObject = Instantiate(origin, new Vector2(xy.x, xy.y), Quaternion.identity);

        if (Team == "Player")
            UnitGameObject.AddComponent<CharaUnit>();
        else if (Team == "Enemy")
            UnitGameObject.AddComponent<EnemyUnit>();

        Unit unit = UnitGameObject.GetComponent<Unit>();
        unit.Data = data;
        unit.Init();
        unit.Ability.Team = Team;
        unit.Ability.GroupID = GroupID;
        unit.name = Team + GroupID;
        
        if (Team == "Player")
            PlayerUnits.Add(unit);
        else if (Team == "Enemy")
            EnemyUnits.Add(unit);        
    }

    public void OnUnitDied(Unit unit)
    {      
        if (unit.Ability.Team == "Player")
        {
            // 기존 사망유닛 재정렬          
            for (int i = 0; i < deadPlayerUnits.Count && i < 1; i++)
            {
                Position pos = (Position)(1 - i);
                deadPlayerUnits[i].Ability.Position = pos;
                deadPlayerUnits[i].transform.position = GetPositionVector(pos);
            }

            // 지금 죽은 유닛 처리
            unit.Ability.State = UnitState.Dead;
            unit.Ability.Position = Position.Back;
            unit.transform.position = GetPositionVector(Position.Back);
            deadPlayerUnits.Add(unit);

            // 살아있는 유닛 재정렬
            alivePlayerUnits = PlayerUnits.Where(u => u.Ability.State == UnitState.Alive).OrderBy(u => u.Ability.Position).ToList();
            for (int i = 0; i < alivePlayerUnits.Count; i++)
            {
                alivePlayerUnits[i].Ability.Position = (Position)i;
                alivePlayerUnits[i].transform.position = GetPositionVector((Position)i);
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
        }
    }

    public Vector2 GetPositionVector(Position pos)
    {
        return pos switch
        {
            Position.Front => new Vector2(-200, 1100),
            Position.Middle => new Vector2(-480, 1100),
            Position.Back => new Vector2(-760, 1100),
            _ => new Vector2(0, 0)
        };
    }
}