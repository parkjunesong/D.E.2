using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Talking : MonoBehaviour
{
    public Text NameField;
    public Text TalkField;
    public Image left;
    public Image front;
    public Image right;
    int index;

    void Start()
    {
        index = 0;
        StartCoroutine(StartTalk(ScenarioManager.Instance.CurrentScenario.Dialogue));
    }
    IEnumerator StartTalk(DialogueData Dialogue)
    {
        NameField.text = Dialogue.lines[index].character.Name;
        TalkField.text = "";
        left.sprite = Dialogue.lines[index].character.GetExpression(Dialogue.lines[index].expression);

        yield return StartCoroutine(Typing(Dialogue.lines[index].text));
        yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Mouse0));

        if (index < Dialogue.lines.Length - 1) 
        {
            index++;
            StartCoroutine(StartTalk(Dialogue));
        }
        else
        {
            switch (ScenarioManager.Instance.CurrentScenario.Type)
            {
                case ScenarioType.OnlyText:
                    {
                        SceneManager.LoadScene("main"); // 시나리오 앤드
                        break;
                    }
                case ScenarioType.BattleLast:
                    {
                        SceneManager.LoadScene("Battle_Test");
                        break;
                    }
            }
        }    
    }

    IEnumerator Typing(string text)
    {       
        foreach (char letter in text.ToCharArray())
        {
            TalkField.text += letter;
            yield return new WaitForSeconds(0.03f);
        }
    }   
}
