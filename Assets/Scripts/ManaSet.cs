using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class manaclass
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

    public manaclass(int i)
    {
        type = i;
        prev = 10;
    }
}

public class ManaSet : MonoBehaviour
{
    public List<manaclass> Mana = new List<manaclass>();
    public int ManaNow;

    void Start()
    {
        for (int i = 0; i < 6; i++)
        {
            gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("마나", typeof(Sprite)) as Sprite;
            Mana.Add(new manaclass(0));
        }
        for (int i = 6; i < 12; i++)
        {           
            gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("프라나", typeof(Sprite)) as Sprite;
            Mana.Add(new manaclass(1));
        }
        ManaNow = Mana.Count;
    }
    public void SetOrder()
    {
        Mana.Sort(delegate (manaclass A, manaclass B)
        {
            if (A.Type < B.Type) return -1;
            else return 1;
        });
    }

    public int[] ManaCount()
    {
        ManaNow = 0;
        int[] ma = new int[4] { 0, 0, 0, 0 };

        SetOrder();
        for (int i = 0; i < 12; i++)
        {
            if (Mana[i].Type == 0)
            {
                ma[0]++;
                gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("마나", typeof(Sprite)) as Sprite;
                ManaNow++;
            }
            else if (Mana[i].Type == 1)
            {
                ma[1]++;
                gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("프라나", typeof(Sprite)) as Sprite;
                ManaNow++;
            }
            else if (Mana[i].Type== 2)
            {
                ma[2]++;
                gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("카르나", typeof(Sprite)) as Sprite;
                ManaNow++;
            }
            else if (Mana[i].Type == 10)
            {
                ma[3]++;
                gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("비활성", typeof(Sprite)) as Sprite;
            }
        }
        return ma;
    }   
    public void ManaCreate(int type) // 마나 생성. 10개 초과시 임시마나로 생성.
    {
        switch (type)
        {
            case (0)://마나
                {
                    gameObject.GetComponentsInChildren<Image>()[ManaNow + 1].sprite = Resources.Load("마나", typeof(Sprite)) as Sprite;
                    Mana[ManaNow].Type = 0;
                    break;
                }
                
            case (1)://프라나
                {
                    break;
                }
            case (2)://카르트
                {                
                    break;
                }
        }
        ManaCount();
    }

    public void ManaChange(int target, int type)
    {
        switch (type)
        {
            case (0):
                {
                    if (Mana[target].Type != 0) 
                    {
                        gameObject.GetComponentsInChildren<Image>()[target + 1].sprite = Resources.Load("마나", typeof(Sprite)) as Sprite;
                        Mana[target].Type = 0;
                    }
                    else
                    {

                    }
                    break;
                }
            case (1):
                {
                    break;
                }
            case (2):
                {
                    break;
                }
        }
        ManaCount();
    }
    public void ManaDelete(int[] target) 
    {
        int[] goal = new int[3] { 0, 0, 0 };

        for (int i = 0; i < 12; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if(Mana[i].Type == j && goal[j] < target[j])
                {
                    gameObject.GetComponentsInChildren<Image>()[i + 1].sprite = Resources.Load("비활성", typeof(Sprite)) as Sprite;
                    Mana[i].Type = 10;
                    Mana[i].Prev = j;
                    goal[j]++;
                    break;
                }
                if (goal[0] >= target[0] && goal[1] >= target[1] && goal[2] >= target[2])
                {
                    ManaCount();
                    return;
                }
                    
            }
        }
       
    }
 
    public void ManaReset()
    {
        for (int i = 0; i < 12; i++)
        {
            if(Mana[i].Type == 10)
            {              
                if (Mana[i].Prev == 0)
                {
                    Mana[i].Type = 1;
                    Mana[i].Prev = 10;
                }
                else if (Mana[i].Prev == 1)
                {
                    Mana[i].Type = 0;
                    Mana[i].Prev = 10;
                }
                else if (Mana[i].Prev == 2)
                {
                    Mana[i].Type = 2;
                    Mana[i].Prev = 10;
                }               
            }           
        }
        ManaCount();
    }

}
