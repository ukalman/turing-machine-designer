using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AlphabetManager : MonoBehaviour
{
    public TMP_InputField inputSymbolsField;
    public TMP_InputField tapeSymbolsField;
    public TMP_Text inputSymbolsText;
    public TMP_Text tapeSymbolsText;
    
    private HashSet<char> inputSymbols = new HashSet<char>();
    private HashSet<char> tapeSymbols = new HashSet<char>();


    private void Start()
    {
        UpdateDisplay();
    }

    // Call to add a symbol to the input alphabet
    public void AddInputSymbol()
    {
        Debug.Log("Input symbol added");
        char symbol;
        if (char.TryParse(inputSymbolsField.text, out symbol))
        {
            if (inputSymbols.Add(symbol))
            {
                tapeSymbols.Add(symbol); // Add to tape symbols if not already present
                UpdateDisplay();
            }
        }
    }

    // Call to add a symbol to the tape alphabet
    public void AddTapeSymbol()
    {
        Debug.Log("Tape symbol added");
        char symbol;
        if (char.TryParse(tapeSymbolsField.text, out symbol))
        {
            if (!inputSymbols.Contains(symbol)) // Only add if not in input symbols
            {
                tapeSymbols.Add(symbol);
                UpdateDisplay();
            }
        }
    }
    

    // Call to remove a symbol from the alphabets
    public void RemoveInputSymbol()
    {
        Debug.Log("Input Symbol Remove");
        char symbol;
        if (char.TryParse(inputSymbolsField.text, out symbol))
        {
            inputSymbols.Remove(symbol);
            tapeSymbols.Remove(symbol);
            UpdateDisplay();
        }
    }
    
    public void RemoveTapeSymbol()
    {
        Debug.Log("Tape Symbol Remove");
        char symbol;
        if (char.TryParse(tapeSymbolsField.text, out symbol))
        {
            if (!inputSymbols.Contains(symbol))
            {
                tapeSymbols.Remove(symbol);
                UpdateDisplay();
            }
           
        }
    }

    // Updates the text display of the alphabets
    private void UpdateDisplay()
    {
        Debug.Log("Update Display");
        inputSymbolsText.text = "{" + string.Join(",", inputSymbols) + "}";
        tapeSymbolsText.text = "{" + string.Join(",", tapeSymbols) + "}";
    }
}