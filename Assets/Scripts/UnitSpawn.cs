using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    public void Spawn(GameObject target, Vector2 xy, string Team)
    {
        GameObject Unit = Instantiate(target, new Vector2(xy.x, xy.y), Quaternion.identity);
        Unit.GetComponent<Unit>().Ability.Team = Team;

        if (Team == "Chara")
        {
            Unit.GetComponent<Unit>().GroupNo = SystemManager.system.CGroup.Count;
            Unit.name = Team + Unit.GetComponent<Unit>().GroupNo;
            gameObject.GetComponent<BattleGroup>().CGroup.Add(Unit);
        }
        else if (Team == "Enemy")
        {
            Unit.GetComponent<Unit>().GroupNo = SystemManager.system.EGroup.Count;
            Unit.name = Team + Unit.GetComponent<Unit>().GroupNo;
            gameObject.GetComponent<BattleGroup>().EGroup.Add(Unit);
        }
        
    }
}
