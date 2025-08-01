using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum CostType { Mana, Prana, Karna, SemiKarna, Used, Universal, Null }

public class Cost
{
    public CostType Type { get; set; }
    public CostType Prev { get; set; }
    public Cost(CostType type)
    {
        Type = type;
        Prev = CostType.Null;
    }
}

public class CostManager : MonoBehaviour
{
    public static CostManager Instance { get; private set; }
    public int CostMax;
    public List<Cost> CostList = new();

    private Transform CostUi;
    private Transform CostPrefab;
    private Sprite[] CostSprite = new Sprite[6];

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        CostMax = 15;
    }
    void Start()
    {
        CostUi = GameObject.Find("코스트보드").transform;
        CostPrefab = CostUi.GetChild(0);
        CostSprite[0] = Resources.Load("Mana", typeof(Sprite)) as Sprite;
        CostSprite[1] = Resources.Load("Prana", typeof(Sprite)) as Sprite;
        CostSprite[2] = Resources.Load("Karna", typeof(Sprite)) as Sprite;
        CostSprite[3] = Resources.Load("SemiKarna", typeof(Sprite)) as Sprite;
        CostSprite[4] = Resources.Load("Used", typeof(Sprite)) as Sprite;
        CostSprite[5] = Resources.Load("Universal", typeof(Sprite)) as Sprite;

        for (int i = 0; i < 6; i++) Cost_Add(CostType.Mana);
        for (int i = 6; i < 12; i++) Cost_Add(CostType.Prana);
    }

    public void CostUse(int[] target)
    {
        int[] goal = new int[3] { 0, 0, 0 };

        // 1. Universal 코스트 우선 사용 (완전 삭제)
        for (int i = CostList.Count - 1; i >= 0; i--)
        {
            var co = CostList[i];
            if (co.Type != CostType.Universal) continue;
            if (goal[0] < target[0]) { CostRemoveForce(i); goal[0]++; continue; }
            if (goal[1] < target[1]) { CostRemoveForce(i); goal[1]++; continue; }
            if (goal[2] < target[2]) { CostRemoveForce(i); goal[2]++; continue; }
        }
        for (int i = 0; i < CostList.Count; i++)
        {
            var co = CostList[i];
            if (co.Type == CostType.Mana && goal[0] < target[0]) { CostChangeForce(i, CostType.SemiKarna); goal[0]++; }
            else if (co.Type == CostType.Prana && goal[1] < target[1]) { CostChangeForce(i, CostType.SemiKarna); goal[1]++; }
            else if ((co.Type == CostType.Karna || co.Type == CostType.SemiKarna) && goal[2] < target[2]) { CostChangeForce(i, CostType.Used); goal[2]++; }
        }
        ImageReset();
    }
    public void Cost_Add(CostType type) // [생성]: 필드 위에 코스트를 추가한다.
    {
        if (CostList.Count >= CostMax) return;

        Cost newCost = new Cost(type);
        CostList.Add(newCost);
        Instantiate(CostPrefab.gameObject, CostUi).SetActive(true);
        ImageReset();
    }
    public void Cost_Remove(int num) // [제거]: 필드 위에 존재하는 코스트를 제거한다.
    {
        if (CostList.Count <= 0) return;

        CostRemoveForce(num);
        ImageReset();
    }
    public void Cost_Change(int num) // [변화]: 필드 위에 존재하는 마나와 프라나의 종류를 바꾼다
    {
        if (CostList[num].Type == CostType.Mana) CostChangeForce(num, CostType.Prana);
        else if (CostList[num].Type == CostType.Prana) CostChangeForce(num, CostType.Mana);
        ImageReset();
    }
    public void Cost_Vanish(int num, bool isFieldEffect = false) // [소멸]: 필드 위의 마나나 프라나를 즉시 카르나로 바꾼다
    {
        if (CostList[num].Type == CostType.Mana || CostList[num].Type == CostType.Prana) 
            CostChangeForce(num, CostType.Karna);

        if (!isFieldEffect)
        {
            foreach (Unit unit in BattleManager.Instance.alivePlayerUnits)
                unit.Passive.OnCostVanished();
            foreach (Unit unit in BattleManager.Instance.EnemyUnits)
                unit.Passive.OnCostVanished();
        }

        ImageReset();
    }
    public void Cost_Regeneration(int num, bool isFieldEffect = false) // [재생]: 필드 위의 카르나를 즉시 마나나 프라나로 바꾼다
    {
        int[] count = CostCount();
        if (CostList[num].Type == CostType.Karna || CostList[num].Type == CostType.SemiKarna)
        {
            if (count[0] > count[1]) CostChangeForce(num, CostType.Prana);
            else if (count[0] < count[1]) CostChangeForce(num, CostType.Mana);
            else
            {
                if(Random.Range(0, 2) == 0)
                {
                    CostChangeForce(num, CostType.Prana);
                }
                else
                {
                    CostChangeForce(num, CostType.Mana);
                }
            }
        }
        if (!isFieldEffect)
        {

        }

        ImageReset();
    }

    public void CostReset()
    {
        int no = 0;
        foreach (Cost co in CostList)
        {
            if (co.Type == CostType.SemiKarna)
            {
                if (co.Prev == CostType.Mana)
                    CostList[no].Type = CostType.Prana;
                else if (co.Prev == CostType.Prana)
                    CostList[no].Type = CostType.Mana;
                else if (co.Prev == CostType.Karna)
                    CostList[no].Type = CostType.Karna;

                CostList[no].Prev = CostType.SemiKarna;
            }
            else if(co.Type == CostType.Used)
            {
                CostList[no].Type = CostType.Karna;
                CostList[no].Prev = CostType.Used;
            }
            no++;
        }
        ImageReset();
    }

    public int[] CostCount()
    {
        int no = 0;
        int[] result = new int[5] { 0, 0, 0, 0, 0 };

        foreach (Cost co in CostList)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (co.Type == (CostType)i)
                {
                    result[i]++;
                    break;
                }
            }
            no++;
        }     
        return result;
    }
    public int CostFindNum(CostType cost)
    {
        return CostList.FindIndex(c => c.Type.Equals(cost));
    }
    public int RandCostFindNum() // 마나, 프라나 중 랜덤 탐색
    {
        int rand = Random.Range(0, 2);
        if (rand == 0) return CostFindNum(CostType.Mana);
        if (rand == 1) return CostFindNum(CostType.Prana);
        return 0;
    }

    void CostChangeForce(int num, CostType change)
    {
        Cost target = CostList[num];
        target.Prev = target.Type;
        target.Type = change;
    }
    void CostRemoveForce(int num)
    {
        if (num < 0 || num >= CostList.Count) return;

        Destroy(CostUi.GetChild(num + 1).gameObject);
        CostList.RemoveAt(num);
    }   
    void ImageReset()
    {
        CostList.Sort((a, b) => a.Type.CompareTo(b.Type));
        var images = CostUi.GetComponentsInChildren<Image>();

        for (int i = 0; i < CostList.Count; i++)
        {
            images[i + 1].sprite = CostSprite[(int)CostList[i].Type];
            images[i + 1].gameObject.SetActive(true);
        }
    }   
}
