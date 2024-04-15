using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class AlphabetManager : MonoBehaviour
{
    
    public HashSet<char> InputSymbols = new HashSet<char>();
    public HashSet<char> TapeSymbols = new HashSet<char>();
    
    public TMP_InputField InputSymbolsField;
    public TMP_InputField TapeSymbolsField;
    public TMP_Text InputSymbolsText;
    public TMP_Text TapeSymbolsText;
    
    private void Start()
    {
        InputSymbolsField.onValueChanged.AddListener(delegate { LimitInputToSingleCharacter(InputSymbolsField); });
        TapeSymbolsField.onValueChanged.AddListener(delegate { LimitInputToSingleCharacter(TapeSymbolsField); });

        UpdateDisplay();
    }
    
    private void LimitInputToSingleCharacter(TMP_InputField inputField)
    {
        if (inputField.text.Length > 1)
        {
            inputField.text = inputField.text.Substring(0, 1);
        }
    }

    // Call to add a symbol to the input alphabet
    private void AddInputSymbol()
    {
        Debug.Log("Input symbol added");
        char symbol;
        if (char.TryParse(InputSymbolsField.text, out symbol))
        {
            if (InputSymbols.Add(symbol))
            {
                TapeSymbols.Add(symbol); // Add to tape symbols if not already present
                UpdateDisplay();
            }
        }
    }

    // Call to add a symbol to the tape alphabet
    private void AddTapeSymbol()
    {
        Debug.Log("Tape symbol added");
        char symbol;
        if (char.TryParse(TapeSymbolsField.text, out symbol))
        {
            if (!InputSymbols.Contains(symbol)) // Only add if not in input symbols
            {
                TapeSymbols.Add(symbol);
                UpdateDisplay();
            }
        }
    }
    

    // Call to remove a symbol from the alphabets
    private void RemoveInputSymbol()
    {
        Debug.Log("Input Symbol Remove");
        char symbol;
        if (char.TryParse(InputSymbolsField.text, out symbol))
        {
            InputSymbols.Remove(symbol);
            TapeSymbols.Remove(symbol);
            UpdateDisplay();
        }
    }
    
    private void RemoveTapeSymbol()
    {
        Debug.Log("Tape Symbol Remove");
        char symbol;
        if (char.TryParse(TapeSymbolsField.text, out symbol))
        {
            if (!InputSymbols.Contains(symbol))
            {
                TapeSymbols.Remove(symbol);
                UpdateDisplay();
            }
           
        }
    }

    // Updates the text display of the alphabets
    private void UpdateDisplay()
    {
        Debug.Log("Update Display");
        InputSymbolsText.text = "{" + string.Join(",", InputSymbols) + "}";
        TapeSymbolsText.text = "{" + string.Join(",", TapeSymbols) + "}";
    }

    private bool ValidateInputSymbols()
    {
        // Check if InputSymbols is not empty
        if (InputSymbols.Count == 0)
        {
            Debug.Log("InputSymbols set cannot be empty.");
            return false;
        }
    
        // Check if all elements in InputSymbols are also in TapeSymbols
        foreach (var symbol in InputSymbols)
        {
            if (!TapeSymbols.Contains(symbol))
            {
                Debug.Log($"Symbol '{symbol}' in InputSymbols is not present in TapeSymbols.");
                return false;
            }
        }

        // If all validations pass
        return true;
    }

    private bool ValidateTapeSymbols()
    {
        // Check if TapeSymbols is not empty and contains all InputSymbols
        if (TapeSymbols.Count == 0 || !InputSymbols.IsSubsetOf(TapeSymbols))
        {
            Debug.Log("TapeSymbols set cannot be empty and must contain all InputSymbols.");
            return false;
        }
    
        // If all validations pass
        return true;
    }
    
    public bool AreAlphabetsValid()
    {
        return ValidateInputSymbols() && ValidateTapeSymbols();
    }

    
    
    
    
}