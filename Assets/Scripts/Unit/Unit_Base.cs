using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit_Base : MonoBehaviour
{
    public virtual void Attack() { }
    public virtual void Damaged(int damage) { }
}
