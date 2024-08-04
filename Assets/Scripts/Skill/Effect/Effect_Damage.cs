using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum DType { Normal, Penetrate, True };

public class Effect_Damage : Effect_Base
{
    public DType DamageType;
    public int IgnoreDefence = 0;

    public override void Awake()
    {
        base.Awake();
    }
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
                        BS.EGroup[0].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        BS.RotaList[0].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);

                    break;
                }
            case AType.Back:
                {
                    if (ability.Team == "Chara")
                        BS.EGroup[BS.EGroup.Count-1].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        BS.RotaList[BS.RotaList.Count-1].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);

                    break;
                }
            case AType.Random:
                {
                    if (ability.Team == "Chara")
                        BS.EGroup[Random.Range(0, BS.EGroup.Count)].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
                    else if (ability.Team == "Enemy")
                        BS.RotaList[Random.Range(0, BS.RotaList.Count)].GetComponent<Unit>().Damaged(damage, DamageType, IgnoreDefence);
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
