using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Unit_Ui : MonoBehaviour
{
    public RectTransform PlayerInfo;

    private Transform SelectMark;
    private Slider HpBar;
    private Slider ShildBar;
    private RectTransform ElementSlot;
    private RectTransform BuffSlot;
    private Text CountText;

    private void Awake()
    {
        SelectMark = transform.GetChild(0).GetChild(0);
    }
    public void PlayerSet(Position position, Sprite face)
    {
        PlayerInfo = GameObject.Find(position.ToString()).GetComponent<RectTransform>();
        PlayerInfo.GetChild(0).GetComponent<Image>().sprite = face;
        HpBar = PlayerInfo.GetChild(1).GetComponent<Slider>();
        ShildBar = PlayerInfo.GetChild(2).GetComponent<Slider>();
        BuffSlot = PlayerInfo.GetChild(3).GetComponent<RectTransform>();
        ElementSlot = transform.GetChild(0).GetChild(2).GetComponent<RectTransform>();
    }
    public void EnemySet()
    {
        HpBar = transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Slider>();
        ShildBar = transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Slider>();
        BuffSlot = transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<RectTransform>();
        CountText = transform.GetChild(0).GetChild(1).GetChild(3).GetChild(0).GetComponent<Text>();
        ElementSlot = transform.GetChild(0).GetChild(2).GetComponent<RectTransform>();
    }
    public void UiRotation(Position position)
    {
        switch (position)
        {
            case Position.Front:
                {
                    PlayerInfo.anchoredPosition = new Vector2(53, -407);
                    break;
                }
            case Position.Middle:
                {
                    PlayerInfo.anchoredPosition = new Vector2(-247, -407);
                    break;
                }
            case Position.Back:
                {
                    PlayerInfo.anchoredPosition = new Vector2(-547, -407);
                    break;
                }
        }
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
        SelectMark.gameObject.SetActive(on);
    }

    public void UpdateBuffUI(List<Buff_Base> activeBuffs)
    {
        GameObject template = BuffSlot.GetChild(0).gameObject;
        int buffCount = activeBuffs.Count;
        int iconIndex = 0;

        for (int i = 1; i < BuffSlot.childCount; i++)  // 템플릿을 제외한 모든 버프
        {
            GameObject icon = BuffSlot.GetChild(i).gameObject;

            if (iconIndex < buffCount)
            {
                Buff_Base buff = activeBuffs[iconIndex];
                icon.name = buff.Name;
                icon.GetComponent<Image>().sprite = buff.Icon;
                icon.GetComponentInChildren<Text>().text = buff.Level.ToString();
                icon.SetActive(true);

                // 위치 재조정
                RectTransform rect = icon.GetComponent<RectTransform>();
                rect.anchoredPosition = new Vector2(-90 + iconIndex * 21f, 0);

                iconIndex++;
            }
            else
            {
                icon.SetActive(false); // 더 이상 필요한 아이콘 없음
            }
        }

        // 필요한 아이콘이 부족한 경우 추가 생성
        while (iconIndex < buffCount)
        {
            Buff_Base buff = activeBuffs[iconIndex];
            GameObject icon = Instantiate(template, BuffSlot);
            icon.name = buff.Name;
            icon.GetComponent<Image>().sprite = buff.Icon;
            icon.GetComponentInChildren<Text>().text = buff.Level.ToString();
            icon.SetActive(true);

            RectTransform rect = icon.GetComponent<RectTransform>();
            rect.anchoredPosition = new Vector2(-90 + iconIndex * 21f, 0);

            iconIndex++;
        }
    }
    public void ShowFloatingText(string text, Color color)
    {
        GameObject obj = FloatingTextPool.Instance.Get();
        obj.transform.SetParent(transform.GetChild(0));

        int activeTextCount = transform.GetChild(0).childCount - 3;
        obj.transform.position = transform.position + new Vector3(0, 100 + (30f * activeTextCount), 0);

        Text txt = obj.GetComponentInChildren<Text>();
        txt.text = text;
        txt.color = color;

        StartCoroutine(AnimateText(obj));
    }
    private IEnumerator AnimateText(GameObject obj, float duration = 1f)
    {
        CanvasGroup group = obj.GetComponent<CanvasGroup>();
        RectTransform rect = obj.GetComponent<RectTransform>();

        Vector3 start = rect.anchoredPosition;
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
        FloatingTextPool.Instance.Return(obj);
    }

    public void setElementIcon(List<Element> element)
    {
        for (int i = 0; i < element.Count; i++)
        {
            GameObject icon = Instantiate(ElementSlot.GetChild(0).gameObject, ElementSlot);
            icon.name = element[i].ToString();
            icon.transform.GetComponent<Image>().sprite = Resources.Load("ElementIcon/" + element[i], typeof(Sprite)) as Sprite;
            icon.SetActive(true);
        }
    }
    public void ElementActivation(Unit target)
    {
        var state = target.Ability.ReactionState;
        ShowFloatingText("활성:" + state.Element, Color.yellow);
    }
    public void ElementRepel(Unit target)
    {
        var state = target.Ability.ReactionState;
        ShowFloatingText("반발:" + state.Element, Color.yellow);
        ElementSlot.GetComponent<Image>().color = Color.black;
    }
    public void ElementNone()
    {
        ElementSlot.GetComponent<Image>().color = new Color(1, 1, 1, 10 / 255f);
    }
}
