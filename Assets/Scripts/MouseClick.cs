using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.UI.CanvasScaler;

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
        List<GameObject> Group = new List<GameObject>();
        if (unit.Ability.Team == "Chara")
        {
            Group = SystemManager.system.CGroup;
            SystemManager.system.SelectedChara = unit.GroupNo;

            foreach(GameObject chara in SystemManager.system.CGroup)
            {
                chara.GetComponent<Unit>().Ui.UpdateSelectIcon(unit.Ability.Team);
            }
        }
        else if (unit.Ability.Team == "Enemy")
        {
            Group = SystemManager.system.EGroup;
            SystemManager.system.SelectedEnemy = unit.GroupNo;
        }
    }
}
