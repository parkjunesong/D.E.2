using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Effect_Base : MonoBehaviour
{
    public float value;
    public abstract void execute();
}
