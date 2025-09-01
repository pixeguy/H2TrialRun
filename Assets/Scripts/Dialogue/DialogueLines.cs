using UnityEngine;

[System.Serializable]
public class DialogueLines
{
    public string line;
    public Character character;

    public DialogueLines(string line, Character character)
    {
        this.line = line;
        this.character = character;
    }
}
