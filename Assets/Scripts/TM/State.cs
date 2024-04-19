
using System.Collections.Generic;
using Enums;

namespace TM
{
    public class State
    {
        // A state has:
        // name (or number)
        // - rules
        // an indicator to specify if it's either an accept or a reject state 

        public string StateName;
        public List<TransitionRule> TransitionRules;
        public StateType Type; // if -1: reject state, if 1: accept state, if 0: neither

        public State(string stateName)
        {
            StateName = stateName;
            TransitionRules = new List<TransitionRule>();

        }

        public void AddRule(TransitionRule transitionRule)
        {
            TransitionRules.Add(transitionRule);
        }
    
    
    } 
}



