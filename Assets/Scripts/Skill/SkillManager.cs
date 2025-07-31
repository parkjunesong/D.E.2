using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public static SkillManager Instance { get; private set; }

    public List<CastingSkill> CastingList = new();
    private GameObject[] skillUi = new GameObject[3];

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 중복 방지
            return;
        }
        Instance = this;

        skillUi[0] = GameObject.Find("스킬1");
        skillUi[1] = GameObject.Find("스킬2");
        skillUi[2] = GameObject.Find("스킬3");
    }
    public void TurnEnd()
    {
        for (int i = CastingList.Count - 1; i >= 0; i--)
        {
            CastingList[i].TickCastingTime();
        }
        Debug.Log(CastingList.Count);
    }


    public void FrontUnitUseSkill(int i)
    {
        BattleManager.Instance.alivePlayerUnits[0].Skill.UseSkill(i, BattleManager.Instance.alivePlayerUnits[0]);
    }
    public void uiReset()
    {
        for (int i = 0; i < 3; i++)
        {
            skillUi[i].transform.GetChild(0).GetComponent<Image>().sprite = BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].Skill_Icon;
            if (BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].currentCoolTime > 0)
            {
                skillUi[i].transform.GetChild(1).gameObject.SetActive(true);
                skillUi[i].transform.GetChild(1).GetChild(0).GetComponent<Text>().text = BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].currentCoolTime.ToString();
            }
            else
            {
                skillUi[i].transform.GetChild(1).gameObject.SetActive(false);
            }
        }             
    }
}
