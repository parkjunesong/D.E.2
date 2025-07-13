using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    int size = 5;
    public GameObject[] Scene = new GameObject[5];

    void Start()
    {
        Scene[0].SetActive(true);
        for (int i = 1; i < size; i++)
            Scene[i].SetActive(false);
    }

    public void TeamSet()
    {
        SceneManager.LoadScene("team");
    }
    public void Goto_Content()
    {
        Scene[0].SetActive(false);
        Scene[1].SetActive(true);
    }
    public void Goto_Story()
    {
        Scene[1].SetActive(false);
        Scene[2].SetActive(true);
    }
    public void Goto_MainStory()
    {
        Scene[2].SetActive(false);
        Scene[3].SetActive(true);
    }    
    public void Goto_MS01()
    {
        Scene[3].SetActive(false);
        Scene[4].SetActive(true);
    }
    public void Back_Content()
    {
        Scene[1].SetActive(false);
        Scene[0].SetActive(true);
    }
    public void Back_Story()
    {
        Scene[2].SetActive(false);
        Scene[1].SetActive(true);
    }
    public void Back_MainStory()
    {
        Scene[3].SetActive(false);
        Scene[2].SetActive(true);
    }
    public void Back_MS01()
    {
        Scene[4].SetActive(false);
        Scene[3].SetActive(true);
    }
}
