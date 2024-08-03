using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public enum AType { Select, Front, Back, Random, Near, All }; //AimType

public abstract class Effect_Base : MonoBehaviour
{
    public float Value;
    public AType AimType;
    protected BattleGroup BG;
    public virtual void Awake()
    {
        BG = GameObject.Find("GameManager").GetComponent<BattleGroup>();
    }
    public abstract void execute(Unit_Ablity ability);
}
