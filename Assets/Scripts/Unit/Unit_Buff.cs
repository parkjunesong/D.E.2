using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;

public class Unit_Buff : MonoBehaviour
{
    private Unit Unit;
    public List<Buff_Base> Buffs = new List<Buff_Base>();

    void Awake()
    {
        Unit = gameObject.GetComponent<Unit>();
    }

    public void OnBuffGained(Buff_Base newBuff)
    {
        Buff_Base existing = Buffs.Find(buff => buff.Name == newBuff.Name);

        if (existing != null) existing.AddStack(1);
        else
        {
            Buff_Base instance = Instantiate(newBuff);
            instance.Apply();
            Buffs.Add(instance);
        }

        Unit.Ability.RecalculBuff(Buffs);
        Unit.Ui.UpdateBuffUI(Buffs);
    }

    public void OnTurnStart()
    {
        foreach (var buff in Buffs)
            buff.TurnStart();

        Unit.Ability.RecalculBuff(Buffs);
        Unit.Ui.UpdateBuffUI(Buffs);
    }

    public void OnTurnEnd()
    {
        for (int i = Buffs.Count - 1; i >= 0; i--)
        {
            var buff = Buffs[i];
            buff.TurnEnd();

            if (buff.Duration <= 0)
            {
                buff.Remove();
                Buffs.RemoveAt(i);
            }
        }
    }
}
