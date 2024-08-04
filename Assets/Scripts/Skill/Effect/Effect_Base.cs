using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum AType { Select, Front, Back, Random, Near, All }; //AimType

public abstract class Effect_Base : MonoBehaviour
{
    public float Value;
    public AType AimType;
    protected BattleSystem BS;
    public virtual void Awake()
    {
        BS = GameObject.Find("GameManager").GetComponent<BattleSystem>();
    }
    public abstract void execute(Unit_Ablity ability);
}
