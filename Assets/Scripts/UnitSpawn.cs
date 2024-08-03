using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    public void Spawn(GameObject target, Vector2 xy)
    {
        GameObject Unit = Instantiate(target, new Vector2(xy.x, xy.y), Quaternion.identity);
        Unit.AddComponent<BoxCollider2D>();
        Unit.AddComponent<SpriteRenderer>();
        Unit.GetComponent<SpriteRenderer>().sprite = Unit.GetComponent<Unit_Animation>().Unit_Standing;
    
    }
}
