using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Animation
{
    private Unit Unit;

    public Unit_Animation(Unit unit)
    {
        Unit = unit;
        Default();
    }

    public void Default()
    {
        Unit.GetComponent<SpriteRenderer>().sprite = Unit.Data.Standing;
    }
}
