namespace Enums
{
    public enum StateType { Normal = 0, Accept = 1, Reject = -1 }
    
    public enum UIPanelTypes
    {
        MainMenu, // Start
        MaxScores,
        Settings,
        TMPreferences,
        TMStateRules,
        TMStartExecution,
        Level,
        Pause,
        Win,
        Fail
    }
    
    public enum GameStates
    {
        None,
        Start,
        TMDesign,
        TMStateRules,
        TMStartExecution,
        LevelPrep,
        Gameplay,
        LevelEnd,
        Pause,
        Win,
        Lose
    }
}

