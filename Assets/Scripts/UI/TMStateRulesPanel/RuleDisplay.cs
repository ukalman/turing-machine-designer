using System.Data;
using System.Text;
using TM;
using TMPro;
using UnityEngine;

namespace UI.TMStateRulesPanel
{
    public class RuleDisplay : MonoBehaviour
    {
        public TMP_Text RuleDisplayText;

        public void SetRuleDetails(TransitionRule rule, string currentStateName)
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
        }        
        
        
        
    }
}