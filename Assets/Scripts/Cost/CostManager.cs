using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    public List<Cost> CostList = new();
    public GameObject CostUi;
    private Sprite[] CostSprite = new Sprite[5];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }

        Instance = this;
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
        for (int i = 0; i < 6; i++) CostList.Add(new Cost(CostType.Mana));    
        for (int i = 6; i < 12; i++) CostList.Add(new Cost(CostType.Prana));
        ImageReset();
    }

    public void CostUse(int[] target)
    {
        int[] goal = new int[3] { 0, 0, 0 };
        while (goal[0] < target[0] || goal[1] < target[1] || goal[2] < target[2])
        {
            int no = 0;
            foreach (Cost co in CostList)
            {
                if (co.Type == CostType.Mana && goal[0] < target[0])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = CostType.SemiKarna;
                    goal[0]++;
                }
                else if (co.Type == CostType.Prana && goal[1] < target[1])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = CostType.SemiKarna;
                    goal[1]++;
                }
                else if ((co.Type == CostType.Karna || co.Type == CostType.SemiKarna) && goal[2] < target[2])
                {
                    CostList[no].Prev = CostList[no].Type;
                    CostList[no].Type = CostType.Used;
                    goal[2]++;
                }
                no++;
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
