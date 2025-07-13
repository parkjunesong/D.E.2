using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor.Experimental.GraphView;
using UnityEditor.Playables;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class SkillManager : MonoBehaviour
{
    public static SkillManager skill;
    public GameObject SkillUi;

    void Awake()
    {
        skill = this;
        SkillUi = GameObject.Find("스킬보드");
    }   
    
    public void MainCharaUseSkill(int i)
    {
        SystemManager.system.MainChara.GetComponent<Unit>().OnSkillUsed(i);
    }
    public void uiReset()
    {
        Unit unit = SystemManager.system.MainChara.GetComponent<Unit>();
        for (int i = 0; i < 3; i++)
        {
            SkillUi.transform.GetChild(i).GetChild(0).GetComponent<Image>().sprite = unit.Skill.Skills[i].Skill_Icon;
            if (unit.Skill.Skills[i].CurrentCooldown > 0)
            {
                SkillUi.transform.GetChild(i).GetChild(1).gameObject.SetActive(true);
                SkillUi.transform.GetChild(i).GetChild(1).GetChild(0).GetComponent<Text>().text = unit.Skill.Skills[i].CurrentCooldown.ToString();
            }
            else
            {
                SkillUi.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
            }
        }             
    }
}
