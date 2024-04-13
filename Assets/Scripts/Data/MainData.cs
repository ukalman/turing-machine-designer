using System;
using System.Collections.Generic;
using UnityEngine.Serialization;

namespace Data
{
    [Serializable]
    public class MainData
    {
        public int StateCount;
        public HashSet<char> InputSymbols;
        public HashSet<char> TapeSymbols;

    }
}