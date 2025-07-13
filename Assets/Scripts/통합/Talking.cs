using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Talking : MonoBehaviour
{
    public static ScenarioData Scenario;
    public Text NameField;
    public Text TalkField;
    public Image left;
    public Image front;
    public Image right;
    int count;
    /*
    void Start()
    {
        Scenario = GotoSC.SelectedScenario;
        count = 0;
        StartCoroutine(StartTalk(Scenario));            
    }
    IEnumerator StartTalk(ScenarioData SC)
    {
        NameField.text = "";
        TalkField.text = "";
        yield return StartCoroutine(Typing(Scenario.Text[count]));
        count++;

        if (count < SC.Num)
            StartCoroutine(StartTalk(Scenario));
        else
            yield break;
        
    }

    IEnumerator Typing(string text)
    {
        int i = 0;
        int a = 0;

        foreach (char letter in text.ToCharArray())
        {
            if (letter == '&') // 대화 세팅
            {
                Image_Data chara = Scenario.CharaList[text[i + 1] - '0'];
                Sprite image = chara.Get_Image[text[i + 3] - '0'];
                int position = text[i + 5] - '0';

                NameField.text = "a";//chara.Name;
                if (position == 0)
                    left.sprite = image;
                else if (position == 1)
                    front.sprite = image;
                else if (position == 2)
                    right.sprite = image;

                a = i - 1;
                i = 0;
                break;
            }
            if (letter == '@') // 전투씬 전환
            {
                CharaList.crm = Scenario.Mapdata.CharaReadyMap;
                SceneManager.LoadScene("team");               

                a = i - 1;
                i = 0;
                yield break;
            }
            i++;
        }

        foreach (char letter in text.ToCharArray())
        {
            TalkField.text += letter;
            yield return new WaitForSeconds(0.01f);

            if (i == a)
                break;
            else
                i++;
        }
        yield return new WaitUntil(() => Input.GetKey(KeyCode.Mouse0));
    }
    */
}
