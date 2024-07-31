using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CostClass
{
    private int type;   
    private int prev;
    public int Type
    {
        get { return type; }
        set { type = value; }
    }
    public int Prev
    {
        get { return prev; }
        set { prev = value; }
    }
    public CostClass(int i)
    {
        type = i;
        prev = 10;
    }
}

public class CostSet : MonoBehaviour
{
    public List<CostClass> Cost = new List<CostClass>();
    Sprite[] CostSprite = new Sprite[5];

    void Start()
    {
        CostSprite[0] = Resources.Load("Mana", typeof(Sprite)) as Sprite;
        CostSprite[1] = Resources.Load("Prana", typeof(Sprite)) as Sprite;
        CostSprite[2] = Resources.Load("Karna", typeof(Sprite)) as Sprite;        
        CostSprite[3] = Resources.Load("SemiKarna", typeof(Sprite)) as Sprite;
        CostSprite[4] = Resources.Load("Used", typeof(Sprite)) as Sprite;

        // 초기 세팅 작업
        for (int i = 0; i < 6; i++) Cost.Add(new CostClass(0));    
        for (int i = 6; i < 12; i++) Cost.Add(new CostClass(1));
        ImageReset();
    }

    public void CostUse(int[] target)
    {
        int[] goal = new int[3] { 0, 0, 0 };
        while (goal[0] < target[0] || goal[1] < target[1] || goal[2] < target[2])
        {
            int no = 0;
            foreach (CostClass co in Cost)
            {
                if (co.Type == 0 && goal[0] < target[0])
                {
                    Cost[no].Prev = Cost[no].Type;
                    Cost[no].Type = 3;
                    goal[0]++;
                }
                else if (co.Type == 1 && goal[1] < target[1])
                {
                    Cost[no].Prev = Cost[no].Type;
                    Cost[no].Type = 3;
                    goal[1]++;
                }
                else if ((co.Type == 2 || co.Type == 3) && goal[2] < target[2])
                {
                    Cost[no].Prev = Cost[no].Type;
                    Cost[no].Type = 4;
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
        foreach (CostClass co in Cost)
        {
            if (co.Type == 3)
            {
                if (co.Prev == 0) Cost[no].Type = 1;
                else if (co.Prev == 1) Cost[no].Type = 0;
                else if (co.Prev == 2) Cost[no].Type = 2;
                Cost[no].Prev = 3;
            }
            else if(co.Type == 4)
            {
                Cost[no].Type = 2;     
                Cost[no].Prev = 4;
            }
            no++;
        }
        ImageReset();
    }

    public int[] CostCount()
    {
        int no = 0;
        int[] result = new int[5] { 0, 0, 0, 0, 0 };

        foreach (CostClass co in Cost)
        {
            for (int i = 0; i <= 4; i++)
            {
                if (co.Type == i)
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
        SetOrder();

        int no = 0;
        foreach (CostClass co in Cost)
        {
            for (int i = 0; i <= 4; i++)           
                if (co.Type == i)               
                    gameObject.GetComponentsInChildren<Image>()[no + 1].sprite = CostSprite[i];                               
            no++;
        }
    }
    void SetOrder() 
    {
        Cost.Sort(delegate (CostClass A, CostClass B)
        {
            if (A.Type < B.Type) return -1;
            else return 1;
        });      
    }
}
