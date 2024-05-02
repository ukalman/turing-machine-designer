using System.Data;
using System.Text;
using TM;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UI.TMStateRulesPanel
{
    public class RuleDisplay : MonoBehaviour, IPointerClickHandler
    {
        public TMP_Text RuleDisplayText;
        public int RuleIndex;
        
        public UnityAction<GameObject> OnClicked;

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this.gameObject);
        }
        
        public void SetRuleDetails(TransitionRule rule, string currentStateName, int ruleIndex)
        {
            var ruleDisplayBuilder = new StringBuilder();
            ruleDisplayBuilder.Append("* δ(")
                .Append(currentStateName)
                .Append(",")
                .Append(rule.InputSymbol)
                .Append(") = (")
                .Append(rule.NextState)
                .Append(",")
                .Append(rule.WriteSymbol)
                .Append(",")
                .Append(rule.MoveDirection.ToString())
                .Append(")");
            
            RuleDisplayText.text = ruleDisplayBuilder.ToString();
            RuleIndex = ruleIndex;
        }        
        
        
        
    }
}