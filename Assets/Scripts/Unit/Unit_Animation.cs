using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Animation
{
    public Sprite Face, Standing;

    public Unit_Animation(UnitData data)
    {
        Face = data.Face;
        Standing = data.Standing;
    }

    public void Default(GameObject unit)
    {
        unit.GetComponent<SpriteRenderer>().sprite = Standing;
    }
}
