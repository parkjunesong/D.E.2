using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldEffect : MonoBehaviour
{
    void Start()
    {
    }
    public void FE_Check()
    {
        int[] CostNow = gameObject.GetComponent<CostSet>().CostCount();
        if (CostNow[2] >= CostNow[0] + CostNow[1])
        {
            Debug.Log("KarnaHigh");
        }
        else
        {
            if (CostNow[0] == CostNow[1])
            {
                Debug.Log("balance");
            }
            else if (CostNow[0] > CostNow[1])
            {
                Debug.Log("ManaHigh");
            }
            else if (CostNow[0] < CostNow[1])
            {
                Debug.Log("PranaHigh");
            }
        }
        
    }
}