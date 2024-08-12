using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    private Slider HpBar;
    private Text CountText;
    void Start()
    {
        HpBar = transform.GetChild(0).GetComponent<Slider>();
        CountText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
    }

    public void UpdateHPBar(int inGame, int inData)
    {
        HpBar.value = (float)inGame / inData;
    }
    public void UpdateCountText(int MoveCount)
    {
        CountText.text = MoveCount + "";
        if (MoveCount == 0)
            CountText.color = new Color32(255, 0, 0, 255);
        else if (CountText.color == new Color32(255, 0, 0, 255))
            CountText.color = new Color32(0, 0, 0, 255);
    }
}
