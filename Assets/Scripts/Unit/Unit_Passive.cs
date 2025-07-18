using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit_Passive : MonoBehaviour
{
    protected Unit Unit;
    public int PassiveStack;
    public int MaxStack;
    void Awake()
    {
        Unit = transform.parent.GetComponent<Unit>();
    }
    public virtual void OnTurnStart()
    {

    }
    public virtual void OnTurnEnd()
    {

    }
}
