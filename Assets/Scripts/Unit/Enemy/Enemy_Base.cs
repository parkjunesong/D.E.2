using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy_Base : MonoBehaviour
{
    protected int MoveCount;
    protected Unit unit;
    public virtual void Awake()
    {
        unit = gameObject.GetComponent<Unit>();    
    }
    public virtual void TurnStart() 
    {
        MoveCount--;
    }
    public abstract void TurnEnd();
}
