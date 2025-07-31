using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public enum Element { None, Fate, Order, Creation, Ruin, Chaos, Void };
public enum ElementReactionType { None, Activation, Repel }

public class ElementReactionState
{
    public ElementReactionType Type;
    public Element Element; // 현재 반응중인 속성
    public int RemainTurn;  // 남은 지속 턴

    public ElementReactionState()
    {
        Type = ElementReactionType.None;
        Element = Element.None;
        RemainTurn = 0;
    }
}

public class ElementReactionRules
{
    private Dictionary<Element, Element> opposites = new()
    {
        { Element.Fate, Element.Void },
        { Element.Order, Element.Chaos },
        { Element.Creation, Element.Ruin },
        { Element.Void, Element.Fate },
        { Element.Chaos, Element.Order },
        { Element.Ruin, Element.Creation },
    };

    public bool IsOpposite(Element a, Element b)
    {
        return opposites.ContainsKey(a) && opposites[a] == b;
    }
}

public class ElementManager : MonoBehaviour
{
    public static ElementManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null && Instance != this) { Destroy(gameObject); return; }
        Instance = this;
    }

    public void OnActionExcuted(Unit target)
    {
        var state = target.Ability.ReactionState;
        if (state.Type == ElementReactionType.Activation || state.Type == ElementReactionType.Repel)
        {
            state.RemainTurn--;
            if (state.RemainTurn <= 0)
            {
                target.Ui.ElementNone();
                state.Type = ElementReactionType.None;
            }
        }
    }
    public void OnDamaged(Unit caster, Unit target)
    {
        if (target.Ability.ReactionState.Type == ElementReactionType.Activation)
            Activation(caster, target, target.Ability.ReactionState.Element);
    }

    public ElementReactionType Resolve(Unit caster, Unit target)
    {
        var state = target.Ability.ReactionState;

        if (state.Type == ElementReactionType.Repel)
            return ElementReactionType.None; // 이미 반발 반응중이거나
        if (state.Type == ElementReactionType.Activation && state.Element == caster.Ability.Element)
            return ElementReactionType.None; // 이미 적용된 활성 반응과 같은 속성

        foreach (var targetElement in target.Ability.Elements)
        {
            if (new ElementReactionRules().IsOpposite(caster.Ability.Element, targetElement))
            {
                state.Type = ElementReactionType.Repel;
                state.Element = caster.Ability.Element;
                state.RemainTurn = 1;

                target.Ui.ElementRepel(target);
                return ElementReactionType.Repel;
            }
            if (caster.Ability.Element == targetElement)
            {                          
                state.Type = ElementReactionType.Activation;
                state.Element = caster.Ability.Element;
                state.RemainTurn = 1;

                target.Ui.ElementActivation(target);
                return ElementReactionType.Activation;
            }
        }
        return ElementReactionType.None;
    }
    
    void Repel(Unit caster, Unit Target, Element element)
    {
        switch (element)
        {
            case Element.Fate:
                {
                    break;
                }
            case Element.Order:
                {
                    break;
                }
            case Element.Creation:
                {
                    break;
                }
            case Element.Ruin:
                {
                    break;
                }
            case Element.Chaos:
                {
                    break;
                }
            case Element.Void:
                {
                    break;
                }
        }
    }
    void Activation(Unit caster, Unit target, Element element)
    {
        float damage = 200 * (1 + caster.Ability.SP / (caster.Ability.SP + 500)); // 레벨 계수 포함 예정
        switch (element)
        {
            case Element.Fate:
                {
                    if (Random.Range(0, 100) > (10 + caster.Ability.SP * 0.015f)) break;

                    if (target.Ability.Team == "Player")
                    {
                        foreach (var unit in BattleManager.Instance.alivePlayerUnits)
                            foreach (var skill in unit.Skill.SkillList)
                            {
                                skill.DelayCoolTime(unit, 1);
                            }
                        target.OnDamaged((0.4f * damage), DType.Normal, 0);
                    }
                    else if (target.Ability.Team == "Enemy")
                    {
                        foreach (var unit in BattleManager.Instance.EnemyUnits)
                            unit.Skill.SkillList[0].DelayCoolTime(unit, 1);
                        target.OnDamaged((0.4f * damage), DType.Normal, 0);
                    }
                    break;
                }
            case Element.Order:
                {
                    break;
                }
            case Element.Creation:
                {
                    if (Random.Range(0, 100) > (10 + caster.Ability.SP * 0.015f)) break;

                    if (target.Ability.Team == "Player")
                    {                       
                        target.OnDamaged((0.4f * damage), DType.Normal, 0);
                    }
                    else if (target.Ability.Team == "Enemy")
                    {
                        CostManager.Instance.Cost_Add(CostType.Universal);
                        target.OnDamaged((0.4f * damage), DType.Normal, 0);
                    }
                    break;
                }
            case Element.Ruin:
                {
                    break;
                }
            case Element.Chaos:
                {
                    break;
                }
            case Element.Void:
                {
                    break;
                }
        }
    }
}
