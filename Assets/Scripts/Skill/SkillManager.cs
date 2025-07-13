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
        SkillUi.transform.GetChild(0).GetComponentsInChildren<Image>()[1].sprite = unit.Skill.Skills[0].Skill_Icon;
        //skill.transform.GetChild(1).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[1].Skill_Icon;
        //skill.transform.GetChild(2).GetComponentsInChildren<Image>()[1].sprite = MainChara.Skills[2].Skill_Icon;
    }
}
