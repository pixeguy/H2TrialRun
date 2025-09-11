using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class DialogueLines
{
    public string line;
    public Character character;
    public List<CA_Option> actionsToPlay;

    public DialogueLines(string line, Character character, List<CA_Option> actions)
    {
        this.line = line;
        this.character = character;
        this.actionsToPlay = actions;
    }
}
