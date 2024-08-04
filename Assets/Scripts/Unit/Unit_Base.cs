using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Base : MonoBehaviour
{
    public virtual void Attack(int skillNo) { }
    public virtual void Damaged(float damage, DType dT, int ignore) { }
    public virtual void TurnStart() { }
    public virtual void TurnEnd() { }

}
