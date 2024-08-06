using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Enemy_Base : MonoBehaviour
{
    protected Text MoveCountUi;
    protected int MoveCount;
    protected Unit unit;
    public virtual void Awake()
    {
        unit = gameObject.GetComponent<Unit>();
        MoveCountUi = transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<Text>();
    }
    public virtual void TurnStart() 
    {
        MoveCount--;
        resetUi();
    }
    public abstract void TurnEnd();

    void MoveCount_Delay(int i)
    {
        MoveCount += i;
        resetUi();
    }
    void resetUi()
    {
        MoveCountUi.text = MoveCount + "";
        if (MoveCount == 0) 
            MoveCountUi.color = new Color32(255, 0, 0, 255);
        else if(MoveCountUi.color == new Color32(255, 0, 0, 255))
            MoveCountUi.color = new Color32(0, 0, 0, 255);
    }
}
