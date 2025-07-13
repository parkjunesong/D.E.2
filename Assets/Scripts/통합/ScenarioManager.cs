using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScenarioManager : MonoBehaviour
{
    public static ScenarioManager Instance;

    public ScenarioData CurrentScenario;

    private void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }

        Instance = this;
        DontDestroyOnLoad(this); // 씬 전환 시 유지
    }

    public void SetScenario(ScenarioData scenario)
    {
        CurrentScenario = scenario;
    }
}