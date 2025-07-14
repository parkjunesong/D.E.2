using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{
    public GameObject SkillUi;

    void Awake()
    {
        SkillUi = GameObject.Find("스킬보드");
    }   
    
    public void FrontUnitUseSkill(int i)
    {
        BattleManager.Instance.alivePlayerUnits[0].OnSkillUsed(i);
    }
    public void uiReset()
    {
        for (int i = 0; i < 3; i++)
        {
            SkillUi.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].Skill_Icon;
            if (BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].CurrentCooldown > 0)
            {
                SkillUi.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                SkillUi.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = BattleManager.Instance.alivePlayerUnits[0].Skill.SkillList[i].CurrentCooldown.ToString();
            }
            else
            {
                SkillUi.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }             
    }
}
