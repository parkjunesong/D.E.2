using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum CostType { Mana, Prana, Karna, SemiKarna, Used, Null }

public class Cost
{
    private CostType now;   
    private CostType prev;

    public CostType Type
    {
        get { return now; }
        set { now = value; }
    }
    public CostType Prev
    {
        get { return prev; }
        set { prev = value; }
    }
    public Cost(CostType type)
    {
        now = type;
        prev = CostType.Null;
    }
}

public class CostManager : MonoBehaviour
{
    public static CostManager Instance { get; private set; }
    public int CostMax;
    public List<Cost> CostList = new();
    public GameObject CostUi;
    private Sprite[] CostSprite = new Sprite[5];

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;

        CostMax = 12;
    }
    void Start()
    {
        CostUi = GameObject.Find("코스트보드");
        CostSprite[0] = Resources.Load("Mana", typeof(Sprite)) as Sprite;
        CostSprite[1] = Resources.Load("Prana", typeof(Sprite)) as Sprite;
        CostSprite[2] = Resources.Load("Karna", typeof(Sprite)) as Sprite;
        CostSprite[3] = Resources.Load("SemiKarna", typeof(Sprite)) as Sprite;
        CostSprite[4] = Resources.Load("Used", typeof(Sprite)) as Sprite;

        // 초기 세팅 작업
        for (int i = 0; i < CostMax / 2; i++) CostList.Add(new Cost(CostType.Mana));
        for (int i = CostMax / 2; i < CostMax; i++) CostList.Add(new Cost(CostType.Prana));
        ImageReset();
    }

    public void CostUse(int[] target)
    {
        int[] goal = new int[3] { 0, 0, 0 };
        while (goal[0] < target[0] || goal[1] < target[1] || goal[2] < target[2])
        {
            int num = 0;
            foreach (Cost co in CostList)
            {
                if (co.Type == CostType.Mana && goal[0] < target[0])
                {
                    CostChangeForce(num, CostType.SemiKarna);
                    goal[0]++;
                }
                else if (co.Type == CostType.Prana && goal[1] < target[1])
                {
                    CostChangeForce(num, CostType.SemiKarna);
                    goal[1]++;
                }
                else if ((co.Type == CostType.Karna || co.Type == CostType.SemiKarna) && goal[2] < target[2])
                {
                    CostChangeForce(num, CostType.Used);
                    goal[2]++;
                }
                num++;
            }
        }
        ImageReset();
    }  
    public void Cost_Change(int num) // [변화]: 필드 위에 존재하는 마나와 프라나의 종류를 바꾼다
    {
        if (CostList[num].Type == CostType.Mana) CostChangeForce(num, CostType.Prana);
        else if (CostList[num].Type == CostType.Prana) CostChangeForce(num, CostType.Mana);
        ImageReset();
    }
    public void Cost_Vanish(int num) // [소멸]: 필드 위의 마나나 프라나를 즉시 카르나로 바꾼다
    {
        if (CostList[num].Type == CostType.Mana || CostList[num].Type == CostType.Prana) 
            CostChangeForce(num, CostType.Karna);
        ImageReset();
    }
    public void Cost_Regeneration(int num) // [재생]: 필드 위의 카르나를 즉시 마나나 프라나로 바꾼다
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

    void CostChangeForce(int num, CostType change)
    {
        Cost target = CostList[num];
        target.Prev = target.Type;
        target.Type = change;
    }
    void ImageReset()
    {
        CostList.Sort(delegate (Cost A, Cost B)
        {
            if (A.Type < B.Type) return -1;
            else return 1;
        });

        int no = 0;
        foreach (Cost co in CostList)
        {
            for (int i = 0; i <= 4; i++) // null 제외되있음
                if (co.Type == (CostType)i)
                    CostUi.GetComponentsInChildren<Image>()[no + 1].sprite = CostSprite[i];                    
            no++;
        }
    }   
}
