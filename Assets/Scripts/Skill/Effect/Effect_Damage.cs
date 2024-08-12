using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DType { Normal, Penetrate, True };

public class Effect_Damage : Effect_Base
{
    public DType DamageType;
    public int IgnoreDefence = 0;

    public override void execute(Unit_Ablity ability)
    {
        float damage = Value * ability.AT * (1 + ability.ID / 100);
        if (ability.CR >= Random.Range(0, 100)) damage *= (1 + ability.CD / 100);

        switch (AimType)
        {
            case AType.Select:
                {
                    
                    break;
                }
            case AType.Front:
                {
                    if (ability.Team == "Chara")
                        SystemManager.system.EGroup[0].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        SystemManager.system.CGroup[0].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);

                    break;
                }
            case AType.Back:
                {
                    if (ability.Team == "Chara")
                        SystemManager.system.EGroup[SystemManager.system.EGroup.Count-1].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        SystemManager.system.CGroup[SystemManager.system.CGroup.Count-1].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);

                    break;
                }
            case AType.Random:
                {
                    if (ability.Team == "Chara")
                        SystemManager.system.EGroup[Random.Range(0, SystemManager.system.EGroup.Count)].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        SystemManager.system.CGroup[Random.Range(0, SystemManager.system.CGroup.Count)].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    break;
                }
            case AType.Near:
                {
                   
                    break;
                }
            case AType.All:
                {
                    
                    break;
                }
        }
    }
  
}
