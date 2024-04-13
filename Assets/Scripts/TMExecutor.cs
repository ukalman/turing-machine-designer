using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;

public class TMExecutor : MonoBehaviour
{
    public TuringMachine TuringMachineInstance;
    public string InputString;
    public List<State> MachineStates;

    void Start()
    {
        // Initialize the Turing machine with provided states and input
        TuringMachineInstance = new TuringMachine(MachineStates, InputString);
        StartCoroutine(RunMachine());
    }

    private IEnumerator RunMachine()
    {
        while (TuringMachineInstance.CurrentState.Type == StateType.Normal)
        {
            bool stepSuccessful = TuringMachineInstance.Step();
            if (!stepSuccessful)
            {
                Debug.Log("Machine halted");
                yield break; // Exit if the machine has halted
            }
            yield return null; // Wait for the next frame
        }

        // Final state handling
        if (TuringMachineInstance.CurrentState.Type == StateType.Accept)
        {
            Debug.Log("Input Accepted");
        }
        else if (TuringMachineInstance.CurrentState.Type == StateType.Reject)
        {
            Debug.Log("Input Rejected");
        }
    }

    // Additional MonoBehaviour methods to interact with the Turing machine can be added here
}