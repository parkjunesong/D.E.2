using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeBuff_Bleeding", menuName = "Scriptable Object/DeBuff/Bleeding")]

public class DeBuff_Bleeding : Buff_Base
{
    public override void OnActionPerformed(Unit unit)
    {
        unit.OnDamaged(0.1f * unit.Ability.HP, DType.Normal, 0);
        SubBuff(1);
    }
}
