using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleGroup : MonoBehaviour
{
    public GameObject MainChara;
    public List<GameObject> CGroup = new List<GameObject>();
    public List<GameObject> EGroup = new List<GameObject>();
    public List<GameObject> RotaList = new List<GameObject>();
    public GameObject SelectedChara, SelectedEnemy;
}
