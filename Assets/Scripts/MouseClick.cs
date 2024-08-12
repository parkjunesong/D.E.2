using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
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
                Unit unit = hit.transform.GetComponent<Unit>();
                if (unit.Ability.Team == "Chara")
                {
                    SystemManager.system.SelectedChara = hit.transform.gameObject;
                }
                else if(unit.Ability.Team == "Enemy")
                {
                    SystemManager.system.SelectedEnemy = hit.transform.gameObject;
                }
            }          
        }
    }
}
