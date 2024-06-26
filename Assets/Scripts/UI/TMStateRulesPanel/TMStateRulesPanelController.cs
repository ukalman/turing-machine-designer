using System;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Signals;
using TM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.TMStateRulesPanel
{
    public class TMStateRulesPanelController : MonoBehaviour
    {
        public TMP_Text stateHeaderText;
        public TMP_Dropdown inputSymbolDropdown;
        public TMP_Dropdown transitionStateDropdown;
        public TMP_Dropdown tapeWriteSymbolDropdown;
        public TMP_Dropdown nextDirectionDropdown;
    
        public Button addRuleButton;
        public Button editRuleButton;
        public Button removeRuleButton;
        public Button nextButton;
        public Button previousButton;
        public Button finishButton;
        public Transform rulesListContainer; // The UI parent where you will list the rules

        private List<State> _states;
        //private HashSet<char> _inputSymbols;
        //private HashSet<char> _tapeSymbols;
        private int _currentStateIndex = 0;
    
        public GameObject ruleDisplayPrefab;
        public GameObject selectedRuleDisplay;
        

        private void Start()
        {
            SetupStates();
            PopulateDropdowns();
            UpdateStateDisplay();
            UpdateButtonInteractivity();
        }

        private void Update()
        {
            UpdateButtonInteractivity();
        }

        private void SetupStates()
        {
            _states = DataManager.Instance.States;

        }

        private void UpdateButtonInteractivity()
        {
            if (selectedRuleDisplay != null)
            {
                editRuleButton.interactable = true;
                removeRuleButton.interactable = true;
            }
            else
            {
                editRuleButton.interactable = false;
                removeRuleButton.interactable = false;
            }
        }
        
        public void AddTransitionRule()
        {
            
            var newRule = new TransitionRule
            {
                InputSymbol = GetSelectedInputSymbol(),
                NextState = GetSelectedTransitionState(),
                WriteSymbol = GetSelectedTapeWriteSymbol(),
                MoveDirection = GetSelectedMoveDirection()
            };

            if (_states[_currentStateIndex].TransitionRules.Contains(newRule))
            {
                Debug.Log("Already existing rule!");
                return;    
            }

            _states[_currentStateIndex].AddRule(newRule);
            UpdateRulesListDisplay();
        }
        
        public void RemoveTransitionRule()
        {
            if (selectedRuleDisplay)
            {
                _states[_currentStateIndex].TransitionRules.RemoveAt(selectedRuleDisplay.GetComponent<RuleDisplay>().RuleIndex);
                Destroy(selectedRuleDisplay);
                
                UpdateRulesListDisplay();
                Debug.Log("Rule deleted!");
                return;
            }
            
            Debug.Log("No rule selected!");
            
        }
        
        private void HandleRuleDisplayClicked(GameObject ruleDisplay)
        {

            if (selectedRuleDisplay != null)
            {
                SetRuleDisplayOpacity(selectedRuleDisplay, 0f); 
            }
            
            selectedRuleDisplay = ruleDisplay;
            
            SetRuleDisplayOpacity(selectedRuleDisplay, 1.0f); 
        }

        private void SetRuleDisplayOpacity(GameObject ruleDisplay, float opacity)
        {
            var imageComponent = ruleDisplay.GetComponent<Image>();
            if (imageComponent != null)
            {
                Color color = imageComponent.color;
                color.a = opacity;
                imageComponent.color = color;
            }
        }
        private void UpdateRulesListDisplay()
        {
            Debug.Log("Update rules list display");
            foreach (Transform child in rulesListContainer)
            {
                var ruleDisplayScript = child.GetComponent<RuleDisplay>();
                if (ruleDisplayScript != null)
                {
                    ruleDisplayScript.OnClicked -= HandleRuleDisplayClicked;
                }
            }
    
            // Clear existing rule displays
            foreach (Transform child in rulesListContainer)
            {
                Destroy(child.gameObject);
            }

            // Instantiate a new rule display prefab for each rule in the current state
            for (int i = 0; i < _states[_currentStateIndex].TransitionRules.Count; i++)
            {
                var ruleDisplay = Instantiate(ruleDisplayPrefab, rulesListContainer);
                var ruleDisplayScript = ruleDisplay.GetComponent<RuleDisplay>();
        
                // Subscribe to the OnClicked event
                ruleDisplayScript.OnClicked += HandleRuleDisplayClicked;

                // Set the rule details
                ruleDisplayScript.SetRuleDetails(_states[_currentStateIndex].TransitionRules[i], _states[_currentStateIndex].StateName, i);
            }
            
            /*
            foreach (var rule in _states[_currentStateIndex].TransitionRules)
            {
                var ruleDisplay = Instantiate(ruleDisplayPrefab, rulesListContainer);
         
                var ruleDisplayScript = ruleDisplay.GetComponent<RuleDisplay>();
                ruleDisplayScript.SetRuleDetails(rule, _states[_currentStateIndex].StateName,);
            }
            */
        }
        // Methods for getting selected values from dropdowns
        private char GetSelectedInputSymbol()
        {
            if (inputSymbolDropdown.options.Count > 0 && inputSymbolDropdown.value < inputSymbolDropdown.options.Count)
            {
                string selectedOption = inputSymbolDropdown.options[inputSymbolDropdown.value].text;
                Debug.Log("Selected option: " + selectedOption[0]);
                return selectedOption[0];  
            }
            return ' ';  // Return a default or error value
        }

        private string GetSelectedTransitionState()
        {
            if (transitionStateDropdown.options.Count > 0 && transitionStateDropdown.value < transitionStateDropdown.options.Count)
            {
                string selectedOption = transitionStateDropdown.options[transitionStateDropdown.value].text;
                return selectedOption;
            }
            return "ErrorState";  // Return a default or error value
        }


        private char GetSelectedTapeWriteSymbol()
        {
            if (tapeWriteSymbolDropdown.options.Count > 0 && tapeWriteSymbolDropdown.value < tapeWriteSymbolDropdown.options.Count)
            {
                string selectedOption = tapeWriteSymbolDropdown.options[tapeWriteSymbolDropdown.value].text;
                return selectedOption[0];  
            }
            return ' ';  // Return a default or error value
        }


        private char GetSelectedMoveDirection()
        {
            if (nextDirectionDropdown.options.Count > 0 && nextDirectionDropdown.value < nextDirectionDropdown.options.Count)
            {
                string selectedOption = nextDirectionDropdown.options[nextDirectionDropdown.value].text;
                if (selectedOption == "Left")
                    return 'L';
                else if (selectedOption == "Right")
                    return 'R';
            }
            return 'E';  // Return 'E' for Error, or consider throwing an exception or error handling
        }


        public void GoToNextState()
        {
            Debug.Log("Go to next state clicked.");
            if (_currentStateIndex < _states.Count - 3)
            {
                Debug.Log("True.");
                _currentStateIndex++;
                UpdateStateDisplay();
            }
        }

        public void GoToPreviousState()
        {
            Debug.Log("Go to previous state clicked.");
            if (_currentStateIndex > 0)
            {
                Debug.Log("True.");
                _currentStateIndex--;
                UpdateStateDisplay();
            }
        }

        private void UpdateStateDisplay()
        {
            // Update the state header text and rules list for the current state
            stateHeaderText.text = $"STATE Q{_currentStateIndex}";
            UpdateRulesListDisplay();
        }

        private void PopulateDropdowns()
        {
            inputSymbolDropdown.ClearOptions();
            tapeWriteSymbolDropdown.ClearOptions();
            transitionStateDropdown.ClearOptions();
            nextDirectionDropdown.ClearOptions();
    
            // Assuming 'inputSymbols' and 'tapeSymbols' are available as HashSet<char>
            inputSymbolDropdown.AddOptions(DataManager.Instance.mainData.InputSymbols.Select(sym => sym.ToString()).ToList());
            tapeWriteSymbolDropdown.AddOptions(DataManager.Instance.mainData.TapeSymbols.Select(sym => sym.ToString()).ToList());
            transitionStateDropdown.AddOptions(_states.Select(state => state.StateName).ToList());
        
            var directions = new List<string> { "Left", "Right" };
            nextDirectionDropdown.AddOptions(directions);
        }

        public void OnFinishClicked()
        {
            TMSignals.Instance.OnTMDesigned?.Invoke();
            GameManager.Instance.StartTMExecution();
        }
    
    }
}
