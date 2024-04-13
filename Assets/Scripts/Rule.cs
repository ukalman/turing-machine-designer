using Enums;

public class Rule
{
    public string BeginStateNumber;
    public char ScannedCharacter;
    public string EndStateNumber;
    public char CharacterToWrite;
    public Direction MoveDirection;  // Using the Direction enum: If it is 0, Turing Machine's tape head will scan the leftwise character next, if it is 1, the opposite.


}
