using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;

public class UnitUi : MonoBehaviour
{
    private Unit unit;
    private Slider HpBar;
    private Text CountText;
    private GameObject SelectIcon;
    private Transform BuffSlot;
    private GameObject buffIcon;
    void Awake()
    {
        unit = transform.parent.GetComponent<Unit>();
        HpBar = transform.GetChild(0).GetComponent<Slider>();
        CountText = transform.GetChild(1).GetChild(0).GetComponent<Text>();
        BuffSlot = transform.GetChild(2);
        buffIcon = BuffSlot.transform.GetChild(0).gameObject;
        SelectIcon = transform.GetChild(3).gameObject;
        
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
    public void UpdateBuffUI(List<Buff_Base> activeBuffs)
    {
        foreach (Transform child in BuffSlot)
        {
            if (child.gameObject.name != "bufficon")  // 템플릿은 그대로 두기
            {
                Destroy(child.gameObject);  // 또는 SetActive(false)
            }
        }
        foreach (var buff in activeBuffs)
        {
            var icon = Instantiate(buffIcon, BuffSlot);
            icon.GetComponent<Image>().sprite = buff.Buff_Icon;
            icon.GetComponentInChildren<Text>().text = "x" + buff.currentStack;
            icon.name = buff.Buff_Name;
            icon.SetActive(true);        
        }
    }
}
