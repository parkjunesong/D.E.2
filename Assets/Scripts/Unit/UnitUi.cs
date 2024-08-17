using System.Collections;
using System.Collections.Generic;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    private Unit unit;
    private Slider HpBar;
    private Text CountText;
    private GameObject SelectIcon;
    void Start()
    {
        unit = transform.parent.GetComponent<Unit>();
        HpBar = transform.GetChild(0).GetComponent<Slider>();
        CountText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        SelectIcon = transform.GetChild(2).gameObject;
    }

    public void UpdateHPBar(int inGame, int inData)
    {
        HpBar.value = (float)inGame / inData;
    }
    public void UpdateCountText(int Count)
    {
        CountText.text = Count + "";
        if (Count == 0)
            CountText.color = new Color32(255, 0, 0, 255);
        else if (CountText.color == new Color32(255, 0, 0, 255))
            CountText.color = new Color32(0, 0, 0, 255);
    }
    public void UpdateSelectIcon(string team)
    {
        SelectIcon.SetActive(false);
        if (team == "Chara")
        {
            if (SystemManager.system.SelectedChara == unit.GroupNo)
                SelectIcon.SetActive(true);
        }
        else if(team == "Enemy")
        {

        }
            
    }
}
