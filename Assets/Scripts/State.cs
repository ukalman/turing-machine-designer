
using System.Collections.Generic;
using Enums;

public class State
{
    // A state has:
    // name (or number)
    // - rules
    // an indicator to specify if it's either an accept or a reject state 

    public string StateNumber;
    public List<Rule> Rules;
    public StateType Type; // if -1: reject state, if 1: accept state, if 0: neither

}
