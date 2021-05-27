using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class NPC : MonoBehaviour
{
    public NPCDialogue dialogue;
    public bool isTalking = false;

    //Can the player interact with the NPC?
    public bool active {get; private set;}

    public void Activate(bool active)
    {
        this.active = active;
    }

    public void Interact()
    {
        Managers.Dialogue.SetTalkingNPC(this);
        Managers.Dialogue.StartConversation(dialogue);
    }

    private void OnMouseDown() {
        if (active && !isTalking && !EventSystem.current.IsPointerOverGameObject())
        {
            isTalking = true;
            Interact();
        }
    }
}
