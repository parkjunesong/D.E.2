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

    public void OnActionPerformed(Unit target)
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
}
