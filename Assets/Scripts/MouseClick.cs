using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseClick : MonoBehaviour
{
    RaycastHit2D hit;

    void Update()
    {
        MouseClickDown();
    }

    void MouseClickDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider != null)
            {
                UpdateSelectIcon(hit.transform.GetComponent<Unit>());           
            }          
        }
    }
    void UpdateSelectIcon(Unit unit)
    {       
        if (unit.Ability.Team == "Player")
        {
            BattleManager.Instance.SelectedPlayerUnit = unit;

            foreach(BattleUnit bu in BattleManager.Instance.PlayerUnits)
                bu.Unit.Ui.UpdateSelectIcon(false);
            unit.Ui.UpdateSelectIcon(true);
        }
        else if (unit.Ability.Team == "Enemy")
        {
            BattleManager.Instance.SelectedEnemyUnit = unit;
            foreach(BattleUnit bu in BattleManager.Instance.EnemyUnits)
                bu.Unit.Ui.UpdateSelectIcon(false);
            unit.Ui.UpdateSelectIcon(true);
        }
    }
}
