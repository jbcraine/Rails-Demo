using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResponseOption : MonoBehaviour
{
    GameObject background;
    public int npcResponseID;
    //When this option is selected, the conversation is ended immediately
    public bool endConversation;

    public delegate void OnEndConversationSelect();
    public event OnResponseSelect OnSelect;
    public event OnEndConversationSelect OnEndConversation;
    
    private void Awake() {
        background = transform.Find("Background").gameObject;
    }
    public void OnMouseOver() {
        background.SetActive(true);
    }
    public void OnMouseDown() {
        if (endConversation)
            OnEndConversation();

        if (OnSelect != null)
            OnSelect(new ResponseSelectArgs(npcResponseID));
    }

    public void OnMouseExit() {
        background.SetActive(false);
    }
}
