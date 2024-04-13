using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TuringMachine
{
    public List<State> States { get; set; }
    public List<char> Tape { get; private set; }
    public int CurrentPosition { get; private set; }
    public State CurrentState { get; private set; }

    public TuringMachine(List<State> states, string input)
    {
        States = states;
        Tape = new List<char>(input.ToCharArray());
        CurrentPosition = 0;
        CurrentState = states.Find(s => s.Type == StateType.Normal); 
    }

    public bool Step()
    {
        // Check if the head is within the bounds of the tape and expand if necessary
        if (CurrentPosition < 0 || CurrentPosition >= Tape.Count)
        {
            Tape.Insert(0, '_');  // Assuming '_' is the blank symbol
            Tape.Add('_');
            CurrentPosition = Mathf.Max(CurrentPosition, 0);
        }

        char currentChar = Tape[CurrentPosition];
        Rule applicableRule = CurrentState.Rules.Find(r => r.ScannedCharacter == currentChar);

        if (applicableRule != null)
        {
            // Apply the rule
            Tape[CurrentPosition] = applicableRule.CharacterToWrite;
            CurrentState = States.Find(state => state.StateNumber == applicableRule.EndStateNumber);
            CurrentPosition += (applicableRule.MoveDirection == Direction.Right) ? 1 : -1;
            return true; // A step was successfully made
        }

        return false; // No applicable rule was found, halting execution
    }

    // Additional methods to reset the machine, etc., could be added
}