using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Managers;
using Signals;
using TM;
using TMPro;
using UI.TMStateRulesPanel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TMStateRulesPanelController : MonoBehaviour
{
    public TMP_Text stateHeaderText;
    public TMP_Dropdown inputSymbolDropdown;
    public TMP_Dropdown transitionStateDropdown;
    public TMP_Dropdown tapeWriteSymbolDropdown;
    public TMP_Dropdown nextDirectionDropdown;
    
    public Button addRuleButton;
    public Button nextButton;
    public Button previousButton;
    public Button finishButton;
    public Transform rulesListContainer; // The UI parent where you will list the rules

    private List<State> _states;
    //private HashSet<char> _inputSymbols;
    //private HashSet<char> _tapeSymbols;
    private int _currentStateIndex = 0;
    
    public GameObject ruleDisplayPrefab; 
    

    private void Start()
    {
        SetupStates();
        //SetupInputAndTapeSymbols();
        PopulateDropdowns();
        UpdateStateDisplay();
    }

    private void SetupStates()
    {
        // Assume the state count and symbol sets have been determined
        // This method initializes the states and populates the dropdowns
        _states = DataManager.Instance.States;

    }
    
    /*
    private void SetupInputAndTapeSymbols()
    {
        _inputSymbols = DataManager.Instance.mainData.InputSymbols;
        _tapeSymbols = DataManager.Instance.mainData.TapeSymbols;
    }
    */
    

    public void AddTransitionRule()
    {
        // Create a new rule based on the dropdown selections and add it to the current state
        var newRule = new TransitionRule
        {
            InputSymbol = GetSelectedInputSymbol(),
            NextState = GetSelectedTransitionState(),
            WriteSymbol = GetSelectedTapeWriteSymbol(),
            MoveDirection = GetSelectedMoveDirection()
        };

        _states[_currentStateIndex].AddRule(newRule);
        UpdateRulesListDisplay();
    }

    private void UpdateRulesListDisplay()
    {
        Debug.Log("Update rules list display");
        // Clear existing rule displays
        foreach (Transform child in rulesListContainer)
        {
            Destroy(child.gameObject);
        }

        // Instantiate a new rule display prefab for each rule in the current state
        foreach (var rule in _states[_currentStateIndex].TransitionRules)
        {
            var ruleDisplay = Instantiate(ruleDisplayPrefab, rulesListContainer);
            // Assuming the prefab has a script attached to it that you can use to set the rule details
            
            var ruleDisplayScript = ruleDisplay.GetComponent<RuleDisplay>();
            ruleDisplayScript.SetRuleDetails(rule, _states[_currentStateIndex].StateName);
        }
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
