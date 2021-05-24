using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Pipes;
using UnityEngine.UI;
using System;

public class DialogueManager : MonoBehaviour
{
    private NPCDialogue npcDialogue;
    private string[] currentResponses;
    private int[] currentResponseIDs;
    public NPC talkingNPC {get; private set;}
    bool isTalking = false;
    float distance;
    float curResponseTracker = 0;
    public GameObject player;
    public GameObject dialogueUI;

    public Text npcName;
    public Text npcDialogueText;
    public Button continueButton;
    public bool currentlyPrinting = false;
    public ResponseMenu responseMenu;
    public ScrollRect scroll;

    public GameObject npcDialogueBox;
    public GameObject playerDialogueBox;
    public bool endWithContinue = false;


    // Start is called before the first frame update
    void Start()
    {
        dialogueUI.SetActive(false);
        playerDialogueBox.SetActive(false);
        npcDialogueBox.SetActive(false);
    }

    private void OnMouseOver() {
        distance = Vector3.Distance(player.transform.position, this.transform.position);
        if (distance <= 2.5f)
        {
            //trigger dialogue
            if (Input.GetKeyDown(KeyCode.E) && !isTalking)
            {
                //StartConversation();
            }
            else if(Input.GetKeyDown(KeyCode.E) && isTalking)
            {
                //EndConversation();
            }
        }
    }

    public void StartConversation(NPCDialogue dialogue)
    {
        endWithContinue = false;

        npcDialogue = dialogue;
        SetDialogueUI(true);
        SetNPCDialogueBox(true);
        continueButton.gameObject.SetActive(false);
        npcDialogueBox.gameObject.SetActive(true);
        isTalking = true;

        dialogueUI.SetActive(true);
        npcName.text = npcDialogue.name;
        PrintDialogue(npcDialogue.npcdialogue[0]);
    }

    public void EndConversation()
    {
        isTalking = false;
        SetNPCDialogueBox(false);
        SetPlayerDialogueBox(false);
        dialogueUI.SetActive(false);
        responseMenu.ClearMenu();

        talkingNPC.isTalking = false;
        talkingNPC = null;
    }

    public void Reply(ResponseSelectArgs r)
    {
        responseMenu.ClearMenu();
        SetNPCDialogueBox(true);
        SetPlayerDialogueBox(false);
        PrintDialogue(npcDialogue.npcdialogue[r.npcResponseID]);
    }

    //Move on to the next panel of dialogue.
    //If dialogue is exhausted, then continue to the player response
    //If the end flag is set, then end the conversation
    public void ContinueDialogue()
    {
        continueButton.gameObject.SetActive(false);
        SetNPCDialogueBox(false);

        if (!endWithContinue)
            OpenResponseMenu();
        else
            EndConversation();
    }

    //Parse the passed in string and print the final version
    public void PrintDialogue(string text)
    {
        StartCoroutine(TypewriterEffect(Parse(text)));
    }

    IEnumerator TypewriterEffect(string text)
    {
        currentlyPrinting = true;
        npcDialogueText.text = "";
        foreach (char c in text)
        {
            npcDialogueText.text += c;
            yield return new WaitForSeconds(.02f);
        }

        currentlyPrinting = false;
        yield return new WaitForSeconds(1f);

        continueButton.gameObject.SetActive(true);
    }    

    public void OpenResponseMenu()
    {
        //scroll.content = responseMenu.GenerateMenu(npc.playerDialogue);

        npcDialogueBox.SetActive(false);
        SetPlayerDialogueBox(true);
        responseMenu.GenerateMenu(currentResponses);
    }

    public void SetPlayerDialogueBox(bool active)
    {
        playerDialogueBox.SetActive(active);
    }

    public void SetNPCDialogueBox(bool active)
    {
        npcDialogueBox.SetActive(active);
    }

    public void SetDialogueUI(bool active)
    {
        dialogueUI.SetActive(active);
    }

    //Parse a string of text for flags.
    //When a flag is found, set any appropriate variables and exclude the flag from the updated string which is to be printed.
    private string Parse(string text)
    {
        //While reading a flag, this is true.
        bool readingFlag = false;
        //The string that is to be printed with all flags removed
        string printString = "", flagText = "";
        char flagIndicator = '*';

        foreach(char c in text)
        {
            //Check if the char indicates a flag
            if (c.Equals(flagIndicator))
            {
                //If the terminating flag is found
                if (readingFlag)
                {
                    ReadFlag(flagText);
                    flagText = "";
                    readingFlag = false;
                }
                else
                    readingFlag = true;
                continue;
            }

            if (readingFlag)
            {
                flagText += c;
                continue;
            }
            
            //If not reading a flag, then the char is added to the print string
            printString += c;
            
        }

        return printString;
    }

    private void ReadFlag(string text)
    {
        switch (text[0])
        {
            case 'e':
                endWithContinue = true;
                break;
            case 'r':
                text = text.Substring(2);
                currentResponses = text.Split(' ');
                
                GetResponses(currentResponses);
                break;
            default:
                Debug.Log("Invalid flag");
                break;
        }
    }

    public void SetTalkingNPC(NPC talkingNPC)
    {
        this.talkingNPC = talkingNPC;
    }

    public void GetResponses(string[] textIDs)
    {
        currentResponseIDs = new int[textIDs.Length];
        Debug.Log(textIDs.Length);
        for (int i = 0; i < textIDs.Length; i++)
        {
            
            Debug.Log(textIDs[i]);
            int id = Int32.Parse(textIDs[i]);
            currentResponses[i] = npcDialogue.playerDialogue[id];
        }
    }

    //Add a method for every response option, since the selected option will communicate that way
    //Once an option is selected, flush the thing
}
