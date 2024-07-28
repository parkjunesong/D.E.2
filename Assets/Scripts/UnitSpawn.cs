using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawn : MonoBehaviour
{
    public void Spawn(GameObject target, Vector2 xy)
    {
        GameObject Unit = Instantiate(target, new Vector2(xy.x, xy.y), Quaternion.identity);
        Ablity Unit_Ablity = Unit.GetComponent<Ablity>();

        Unit.AddComponent<BoxCollider2D>();
        Unit.AddComponent<SpriteRenderer>();
        Unit.GetComponent<SpriteRenderer>().sprite = Unit_Ablity.StandingSprite;
    }
}
