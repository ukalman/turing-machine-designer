using System.Data;
using TM;
using TMPro;
using UnityEngine;

namespace UI.TMStateRulesPanel
{
    public class RuleDisplay : MonoBehaviour
    {
        public TMP_Text RuleDisplayText;

        private string _ruleDisplayText;

        public void SetRuleDetails(TransitionRule rule, string currentStateName)
        {
            _ruleDisplayText = "(";
            _ruleDisplayText += currentStateName;
            _ruleDisplayText += ",";
            _ruleDisplayText += rule.InputSymbol;
            _ruleDisplayText += ") = (";
            _ruleDisplayText += rule.NextState;
            _ruleDisplayText += ",";
            _ruleDisplayText += rule.WriteSymbol;
            _ruleDisplayText += ",";
            _ruleDisplayText += rule.MoveDirection.ToString();
            _ruleDisplayText += ")";

            RuleDisplayText.text = _ruleDisplayText;


        }
        
        
        
    }
}