using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class StateCounter : MonoBehaviour
{
    public TMP_InputField stateCountInputField; // The input field for state count
    public Button incrementButton;
    public Button decrementButton;
    
    [FormerlySerializedAs("_stateCount")] public int StateCount = 1;
    private const int MinStateCount = 1;
    private const int MaxStateCount = 30;

    void Start()
    {
        // Add listener for the input field to handle the user's keyboard input
        stateCountInputField.onValueChanged.AddListener(delegate { StateCountChanged(); });

        // Initialize input field value
        stateCountInputField.text = StateCount.ToString();

        // Optionally, disable the buttons if the count is at the min or max
        UpdateButtonInteractivity();
    }

    // Update button interactivity based on the current state count
    private void UpdateButtonInteractivity()
    {
        incrementButton.interactable = StateCount < MaxStateCount;
        decrementButton.interactable = StateCount > MinStateCount;
    }

    // Call this method when the increment button is clicked or input field is increased
    private void IncrementStateCount()
    {
        if (StateCount < MaxStateCount)
        {
            StateCount++;
            UpdateStateCountDisplay();
        }
    }

    // Call this method when the decrement button is clicked or input field is decreased
    private void DecrementStateCount()
    {
        if (StateCount > MinStateCount)
        {
            StateCount--;
            UpdateStateCountDisplay();
        }
    }

    // Update the UI input field display
    private void UpdateStateCountDisplay()
    {
        stateCountInputField.text = StateCount.ToString();
        UpdateButtonInteractivity();
    }

    // Validate and apply the state count when changed from the keyboard
    private void StateCountChanged()
    {
        if (IsStateCountValid())
        {
            // The input is valid, so update the state count
            StateCount = int.Parse(stateCountInputField.text);
            UpdateButtonInteractivity();
        }
        else
        {
            // The input is not valid, reset to the last valid state count
            stateCountInputField.text = StateCount.ToString();
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