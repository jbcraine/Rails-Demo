using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public struct DialogueOption
{
    public string dialogue {get;}
    public bool endsConversation {get;}
}

[CreateAssetMenu(fileName = "NPC file", menuName = "NPC Files Archive")]
public class NPCDialogue : ScriptableObject
{
    public string name;
    [TextArea(3, 15)]
    public string[] npcdialogue;

    [TextArea(3, 15)]
    public string[] playerDialogue;
}
