using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.UI;

public class SystemManager : MonoBehaviour
{
    public static SystemManager system;
    public int Turn;
    GameObject TurnUi;
    public GameObject MainChara;
    public List<GameObject> CGroup = new List<GameObject>();
    public List<GameObject> EGroup = new List<GameObject>();
    public List<GameObject> RotaList = new List<GameObject>();
    public int SelectedChara, SelectedEnemy;

    public ScenarioData data;

    void Awake()
    {
        system = this;
    }
    private void Start()
    {
        //data = ScenarioManager.Instance.CurrentScenario;

        if (data != null)
        {
            MapMaker(data.MapToLoad);

            foreach (GameObject chara in CGroup)
            {
                Unit unit = chara.GetComponent<Unit>();
                if (unit.Ability.UT == UnitType.Unit_Alive)
                {
                    RotaList.Add(chara);
                }
            }

            MainChara = RotaList[0];

            Turn = 0;
            TurnUi = GameObject.Find("Turn");

            TurnStart();
        }
    }
    
    public void MapMaker(MapData map)
    {
        int i = 0;
        foreach (UnitData data in map.CGroup)
        {
            UnitData uData = Instantiate(data);
            GameObject.Find("GameManager").GetComponent<UnitSpawn>().InitSpawn(uData, new Vector2(-200 - 280 * i, 1100), "Chara");
            CGroup[i].GetComponent<Unit>().Ability.GroupNo = i;
            CGroup[i].GetComponent<Unit>().name = "Chara" + i;
            CGroup[i].GetComponent<Unit>().Ability.Team = "Chara";

            i++;
        }
        i = 0;
        foreach (UnitData data in map.EGroup)
        {
            UnitData uData = Instantiate(data);
            GameObject.Find("GameManager").GetComponent<UnitSpawn>().InitSpawn(uData, new Vector2(200 + 280 * i, 1100), "Enemy");
            EGroup[i].GetComponent<Unit>().Ability.GroupNo = i;
            EGroup[i].GetComponent<Unit>().name = "Enemy" + i;
            EGroup[i].GetComponent<Unit>().Ability.Team = "Enemy";

            i++;
        }
    }

    public void TurnStart()
    {
        Turn++;
        TurnUi.GetComponent<Text>().text = Turn + " Turn";
        SkillManager.skill.uiReset();

        for (int i = 0; i < CGroup.Count; i++)
        {
            CGroup[i].GetComponent<Unit>().TurnStart();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnStart();
        }
    }
    public void TurnEnd()
    {
        for (int i = 0; i < CGroup.Count; i++)
        {
            CGroup[i].GetComponent<Unit>().TurnEnd();
        }
        for (int i = 0; i < EGroup.Count; i++)
        {
            EGroup[i].GetComponent<Unit>().TurnEnd();
        }
        TurnStart();
    }
    public void Rotation()
    {
        GameObject temp = RotaList[0];
        RotaList.RemoveAt(0);
        RotaList.Add(temp);

        int j = 0;
        for (int i = 0; i < CGroup.Count; i++)
        {
            Unit unit = CGroup[i].GetComponent<Unit>();
            if (unit.Ability.UT == UnitType.Unit_Alive)
            {
                CGroup[i] = RotaList[j];
                j++;
            }
        }
        foreach (GameObject chara in CGroup)
        {
            Unit unit = chara.GetComponent<Unit>();
            unit.Ability.GroupNo = CGroup.IndexOf(chara);
            chara.transform.position = new Vector2(-200 - 280 * unit.Ability.GroupNo, 1100);
        }
        MainChara = RotaList[0];

        CostManager.cost.CostReset();
        TurnEnd();
    }
}
