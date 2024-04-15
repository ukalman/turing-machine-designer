using System.Collections.Generic;
using Extensions;
using UnityEngine.Events;

namespace Signals
{
    public class TMSignals : MonoSingleton<TMSignals>
    {
        public UnityAction<int,HashSet<char>,HashSet<char>> OnTMPreferencesDetermined = delegate { };
        public UnityAction OnTMStateRulesDetermined = delegate { };
        public UnityAction OnTMDesigned = delegate { };
    }
}