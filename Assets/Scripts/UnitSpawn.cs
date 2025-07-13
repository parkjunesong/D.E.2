using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class UnitSpawn : MonoBehaviour
{
    public GameObject origin;
    public void Spawn(UnitData data, Vector2 xy, string Team)
    {
        GameObject Unit = Instantiate(origin, new Vector2(xy.x, xy.y), Quaternion.identity);
        
        if (Team == "Chara")
            Unit.AddComponent<CharaUnit>();
        else if (Team == "Enemy")
            Unit.AddComponent<EnemyUnit>();

        Unit.GetComponent<Unit>().Data = data;
        Unit.GetComponent<Unit>().Init();

        Unit.name = Team + Unit.GetComponent<Unit>().Ability.GroupNo;
        Unit.GetComponent<Unit>().Ability.Team = Team;
        if (Team == "Chara")
        {
            Unit.GetComponent<Unit>().Ability.GroupNo = SystemManager.system.CGroup.Count;
            gameObject.GetComponent<SystemManager>().CGroup.Add(Unit);
        }
        else if (Team == "Enemy")
        {
            Unit.GetComponent<Unit>().Ability.GroupNo = SystemManager.system.EGroup.Count;
            gameObject.GetComponent<SystemManager>().EGroup.Add(Unit);
        }             
    }
}
