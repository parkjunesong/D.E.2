using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "Cronoa_Alpha_Skill1", menuName = "Scriptable Object/SkillData/CronoaAlpha_Skill1", order = int.MaxValue)]
public class Skill_CronoaAlpha_1 : Skill_Base
{
    public override void Execute(Unit caster, List<Unit> targets)
    {
        Effects[0].Execute(caster, targets); // Èû ¹öÇÁ È¹µæ
        Effects[1].Execute(caster, targets); // ½Ã°£ÀÇ Åé´Ï¹ÙÄû ½ºÅÃ +1
        if (caster.GetComponent<CharaUnit>().Position == Position.Front)
        {
            Effects[1].Execute(caster, targets); // ½Ã°£ÀÇ Åé´Ï¹ÙÄû ½ºÅÃ Ãß°¡ È¹µæ
        }
    }
}