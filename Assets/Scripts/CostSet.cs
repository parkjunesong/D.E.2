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
    Sprite[] CostSprite = new Sprite[4];

    void Start()
    {
        CostSprite[0] = Resources.Load("Mana", typeof(Sprite)) as Sprite;
        CostSprite[1] = Resources.Load("Prana", typeof(Sprite)) as Sprite;
        CostSprite[2] = Resources.Load("Karna", typeof(Sprite)) as Sprite;
        CostSprite[3] = Resources.Load("Used", typeof(Sprite)) as Sprite;

        // 초기 세팅 작업
        for (int i = 0; i < 6; i++)
        {
            Cost.Add(new CostClass(0));
            gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = CostSprite[Cost[i].Type];
        }
        for (int i = 6; i < 12; i++)
        {
            Cost.Add(new CostClass(1));
            gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = CostSprite[Cost[i].Type];
        }
    }

    public void CostUse(int[] target)
    {
        int no = 0;
        int[] goal = new int[3] { 0, 0, 0 };

        foreach (CostClass co in Cost)
        {
            if (goal[0] >= target[0] && goal[1] >= target[1] && goal[2] >= target[2])
                return;
            else
                for (int i = 0; i < 3; i++)
                {
                    if (co.Type == i && goal[i] < target[i])
                    {
                        gameObject.GetComponentsInChildren<Image>()[no + 1].sprite = CostSprite[3];
                        Cost[no].Prev = Cost[no].Type;
                        Cost[no].Type = 10;
                        goal[i]++;
                        break;
                    }
                }
            no++;
        }
        SetOrder();
    }

    public void CostReset()
    {
        int no = 0;
        foreach (CostClass co in Cost)
        {
            if (co.Type == 10)
            {          
                if (co.Prev == 0) Cost[no].Type = 1;
                else if (co.Prev == 1) Cost[no].Type = 0;
                else if (co.Prev == 2) Cost[no].Type = 2;
                Cost[no].Prev = 10;
            }
            no++;
        }
        SetOrder();
    }


    public int[] CostCount()
    {
        int[] result = new int[4] { 0, 0, 0, 0 };

        foreach (CostClass co in Cost)
        {
            if (co.Type == 10) // 10번은 비활성 코스트(사용 후)
                result[3]++;
            else
                for (int i = 0; i < 3; i++)
                {
                    if (co.Type == i)
                    {
                        result[i]++;
                        break;
                    }
                }
        }
        return result;
    }

    void SetOrder() // 양옆 끝부터 카르나 생기게 변경
    {
        Cost.Sort(delegate (CostClass A, CostClass B)
        {
            if (A.Type < B.Type) return -1;
            else return 1;
        });
    }
}
