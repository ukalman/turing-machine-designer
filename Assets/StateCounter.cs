using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StateCounter : MonoBehaviour
{
    public TMP_InputField stateCountInputField; // The input field for state count
    public Button incrementButton;
    public Button decrementButton;
    
    private int _stateCount = 1;
    private const int MinStateCount = 1;
    private const int MaxStateCount = 30;

    void Start()
    {
        // Add listener for the input field to handle the user's keyboard input
        stateCountInputField.onValueChanged.AddListener(delegate { StateCountChanged(); });

        // Initialize input field value
        stateCountInputField.text = _stateCount.ToString();

        // Optionally, disable the buttons if the count is at the min or max
        UpdateButtonInteractivity();
    }

    // Update button interactivity based on the current state count
    private void UpdateButtonInteractivity()
    {
        incrementButton.interactable = _stateCount < MaxStateCount;
        decrementButton.interactable = _stateCount > MinStateCount;
    }

    // Call this method when the increment button is clicked or input field is increased
    private void IncrementStateCount()
    {
        if (_stateCount < MaxStateCount)
        {
            _stateCount++;
            UpdateStateCountDisplay();
        }
    }

    // Call this method when the decrement button is clicked or input field is decreased
    private void DecrementStateCount()
    {
        if (_stateCount > MinStateCount)
        {
            _stateCount--;
            UpdateStateCountDisplay();
        }
    }

    // Update the UI input field display
    private void UpdateStateCountDisplay()
    {
        stateCountInputField.text = _stateCount.ToString();
        UpdateButtonInteractivity();
    }

    // Validate and apply the state count when changed from the keyboard
    private void StateCountChanged()
    {
        if (IsStateCountValid())
        {
            // The input is valid, so update the state count
            _stateCount = int.Parse(stateCountInputField.text);
            UpdateButtonInteractivity();
        }
        else
        {
            // The input is not valid, reset to the last valid state count
            stateCountInputField.text = _stateCount.ToString();
        }
    }

    public bool IsStateCountValid()
    {
        int parsedStateCount;
        // Try to parse the input text to an integer and check if it's within the specified range
        if (int.TryParse(stateCountInputField.text, out parsedStateCount) &&
            parsedStateCount >= MinStateCount &&
            parsedStateCount <= MaxStateCount)
        {
            // Input is acceptable: it's an integer within the range of 1 to 30
            return true;
        }
        else
        {
            // Input is not acceptable
            return false;
        }
    }
}