using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSelectUI : MonoBehaviour
{
    public void ScenarioSelect(ScenarioData scenario)
    {
        ScenarioManager.Instance.SetScenario(scenario);
        if(scenario.Type == ScenarioType.OnlyText || scenario.Type == ScenarioType.BattleLast)
            SceneManager.LoadScene("talk");
        if (scenario.Type == ScenarioType.OnlyBattle || scenario.Type == ScenarioType.BattleFirst)
            SceneManager.LoadScene("Battle_Test");
    }
}
