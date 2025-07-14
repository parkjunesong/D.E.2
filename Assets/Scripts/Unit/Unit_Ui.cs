using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Ui : MonoBehaviour
{
    private Unit Unit;
    private Slider HpBar;
    private Slider ShildBar;
    private Text CountText;
    private Transform SelectIcon;
    private Transform BuffSlot;

    void Awake()
    {
        Unit = gameObject.GetComponent<Unit>();
        Transform info = transform.GetChild(0).GetChild(0);

        HpBar = info.GetChild(0).GetComponent<Slider>();
        ShildBar = info.GetChild(1).GetComponent<Slider>();
        BuffSlot = info.GetChild(2);

        CountText = gameObject.transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        SelectIcon = gameObject.transform.GetChild(0).GetChild(2);
    }

    public void UpdateHPBar(int inGame, int inData)
    {
        HpBar.value = (float)inGame / inData;
    }
    public void UpdateShildBar(int inGame, int inData)
    {
        ShildBar.value = (float)inGame / inData;       
    }
    public void UpdateCountText(int Count)
    {
        CountText.text = Count + "";
        if (Count == 0)
            CountText.color = new Color32(255, 0, 0, 255);
        else if (CountText.color == new Color32(255, 0, 0, 255))
            CountText.color = new Color32(0, 0, 0, 255);
    }   
    public void UpdateSelectIcon(bool on)
    {
        SelectIcon.gameObject.SetActive(on);
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
            var icon = Instantiate(BuffSlot.GetChild(0).gameObject, BuffSlot);
            icon.GetComponent<Image>().sprite = buff.Buff_Icon;
            icon.GetComponentInChildren<Text>().text = "x" + buff.currentStack;
            icon.name = buff.Buff_Name;
            icon.SetActive(true);   
        }
    }
}
