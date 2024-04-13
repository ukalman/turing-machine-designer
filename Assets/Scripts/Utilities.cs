namespace Enums
{
    public enum Direction { Left = 0, Right = 1 }
    public enum StateType { Normal = 0, Accept = 1, Reject = -1 }
    
    public enum UIPanelTypes
    {
        MainMenu, // Start
        MaxScores,
        Settings,
        TMPreferences,
        Level,
        Pause,
        Win,
        Fail
    }
    
    public enum GameStates
    {
        None,
        Start,
        LevelPrep,
        Gameplay,
        LevelEnd,
        Pause,
        Win,
        Lose
    }
}

