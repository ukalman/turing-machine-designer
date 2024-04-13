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
    public void IncrementStateCount()
    {
        if (_stateCount < MaxStateCount)
        {
            _stateCount++;
            UpdateStateCountDisplay();
        }
    }

    // Call this method when the decrement button is clicked or input field is decreased
    public void DecrementStateCount()
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
    public void StateCountChanged()
    {
        // Validate the input and correct it if necessary
        if (!int.TryParse(stateCountInputField.text, out _stateCount))
        {
            _stateCount = MinStateCount; // Default to min if parse fails
        }
        
        _stateCount = Mathf.Clamp(_stateCount, MinStateCount, MaxStateCount);
        stateCountInputField.text = _stateCount.ToString(); // Correct the displayed value
        UpdateButtonInteractivity();
    }
}