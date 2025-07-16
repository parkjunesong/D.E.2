using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit_Ui : MonoBehaviour
{
    private Slider HpBar;
    private Slider ShildBar;
    private Text CountText;
    private Transform SelectIcon;
    private Transform BuffSlot;
    private Transform unitCanvas;
    private GameObject floatingText;

    void Awake()
    {
        unitCanvas = transform.GetChild(0);

        HpBar = unitCanvas.GetChild(0).GetChild(0).GetComponent<Slider>();
        ShildBar = unitCanvas.GetChild(0).GetChild(1).GetComponent<Slider>();
        BuffSlot = unitCanvas.GetChild(0).GetChild(2);
        CountText = unitCanvas.GetChild(1).GetChild(0).GetComponent<Text>();
        SelectIcon = unitCanvas.GetChild(2);
        floatingText = unitCanvas.GetChild(3).gameObject;
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
    public void ShowFloatingText(string text, Color color)
    {    
        GameObject obj = Instantiate(floatingText, unitCanvas);
        obj.transform.position = transform.position + new Vector3(0, 150, 0);

        // 텍스트 설정
        Text txt = obj.GetComponentInChildren<Text>();
        txt.text = text;
        txt.color = color;

        StartCoroutine(AnimateText(obj));
    }
    private IEnumerator AnimateText(GameObject obj, float duration = 1f)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        RectTransform rect = obj.GetComponent<RectTransform>();
        int rand = Random.Range(-20, 20);

        Vector3 start = rect.anchoredPosition + new Vector2(rand, rand);
        Vector3 end = start + new Vector3(0, 30, 0);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / duration;

            rect.anchoredPosition = Vector3.Lerp(start, end, t);
            if (group != null)
                group.alpha = 1f - t;

            yield return null;
        }
        Destroy(obj);
    }
}
