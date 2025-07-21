using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Buff_Ability", menuName = "Scriptable Object/Buff/Ability")]
public class Buff_Ability : Buff_TurnEndBased
{
    public override void OnApply(int stack)
    {
        base.OnApply(stack);
    }
    public override void AddBuff(int stack)
    {
        base.AddBuff(stack);
    }
}