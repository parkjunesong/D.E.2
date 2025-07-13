using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenarioSelectUI : MonoBehaviour
{
    public void ScenarioSelect(ScenarioData scenario)
    {
        ScenarioManager.Instance.SetScenario(scenario);
        SceneManager.LoadScene("Battle_Test");
    }
}
