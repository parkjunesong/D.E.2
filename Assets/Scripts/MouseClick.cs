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
        if (unit.Ability.State == UnitState.Dead)
            Debug.Log("Dead Unit");
        else
        {
            if (unit.Ability.Team == "Player")
            {
                BattleManager.Instance.SelectedPlayerUnit = unit;

                foreach (Unit bu in BattleManager.Instance.alivePlayerUnits)
                    bu.Ui.UpdateSelectIcon(false);
                unit.Ui.UpdateSelectIcon(true);
            }
            else if (unit.Ability.Team == "Enemy")
            {
                BattleManager.Instance.SelectedEnemyUnit = unit;
                foreach (Unit bu in BattleManager.Instance.EnemyUnits)
                    bu.Ui.UpdateSelectIcon(false);
                unit.Ui.UpdateSelectIcon(true);
            }
        }    
    }
}
