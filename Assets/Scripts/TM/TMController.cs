using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Enums;
using Managers;
using Signals;

namespace TM
{
    public class TMController : MonoBehaviour
    {
        private TuringMachine _tm;
        public string InputString;
        private List<State> _machineStates;

        void Start()
        {
            TMSignals.Instance.OnTMExecuted += SetupAndExecuteTM;
            _machineStates = DataManager.Instance.States;
            //_tm = new TuringMachine(_machineStates, InputString);
            // StartCoroutine(RunMachine());
        }

        private void OnDestroy()
        {
            TMSignals.Instance.OnTMExecuted -= SetupAndExecuteTM;
        }

        private void SetupAndExecuteTM()
        {
            InputString = DataManager.Instance.mainData.InputString;
            Debug.Log("Input string is " + InputString);
            _tm = new TuringMachine(_machineStates, InputString);
            StartCoroutine(RunTM());
        }
        

        private IEnumerator RunTM()
        {
            while (_tm.CurrentState.Type == StateType.Normal)
            {
                bool stepSuccessful = _tm.Step();
                if (!stepSuccessful)
                {
                    Debug.Log("Machine halted");
                    TMSignals.Instance.OnTMHalted?.Invoke(false);
                    yield break; // Exit if the machine has halted
                }
                yield return null; // Wait for the next frame
            }

            // Final state handling
            if (_tm.CurrentState.Type == StateType.Accept)
            {
                TMSignals.Instance.OnTMHalted?.Invoke(true);
                Debug.Log("Input Accepted");
            }
            else if (_tm.CurrentState.Type == StateType.Reject)
            {
                TMSignals.Instance.OnTMHalted?.Invoke(false);
                Debug.Log("Input Rejected");
            }
        }

        // Additional MonoBehaviour methods to interact with the Turing machine can be added here
    }    
}

