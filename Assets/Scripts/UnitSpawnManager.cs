using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitSpawnManager : MonoBehaviour
{
    public static UnitSpawnManager Instance { get; private set; }

    [SerializeField]
    private GameObject PlayerPrefab;
    [SerializeField]
    private GameObject EnemyPrefab;

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void Spawn(UnitData data, int count, string team)
    {
        if (team == "Player")
        {
            UnitData uData = Instantiate(data);
            GameObject UnitGameObject = Instantiate(PlayerPrefab, new Vector2(0, 0), Quaternion.identity);

            UnitGameObject.transform.position = UnitManager.Instance.GetPositionPlayer((Position)count);
            Unit unit = UnitGameObject.GetComponent<Unit>();
            unit.Data = uData;
            unit.Init();
            unit.Ability.Team = team;
            unit.name = unit.Ability.Name;
            unit.GetComponent<PlayerUnit>().Position = (Position)count;
            unit.Ui.PlayerSet(unit.GetComponent<PlayerUnit>().Position, data.Face);
            unit.Ui.setElementIcon(unit.Ability.Elements);

            UnitManager.Instance.PlayerUnits.Add(unit);
        }
        else if (team == "Enemy")
        {
            UnitData uData = Instantiate(data);
            GameObject UnitGameObject = Instantiate(EnemyPrefab, new Vector2(0, 0), Quaternion.identity);

            UnitGameObject.transform.position = UnitManager.Instance.GetPositionEnemy(count);
            Unit unit = UnitGameObject.GetComponent<Unit>();
            unit.Data = uData;
            unit.Init();
            unit.Ability.Team = team;
            unit.name = unit.Ability.Name;
            unit.Ui.EnemySet();
            unit.Ui.setElementIcon(unit.Ability.Elements);

            UnitManager.Instance.EnemyUnits.Add(unit);
        }
    }
}
