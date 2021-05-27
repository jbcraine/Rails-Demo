using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ResponseMenu : MonoBehaviour
{
    public Image responseOption;
    List<Image> responseBoxes;
    public RectTransform responseMenu;
    public float maxLinesPerWindow;

    //Create the response menu according to the responses provided
    //Every response is in its own panel/text box. These responses are accumulated in a larger dialogue window.
    public void GenerateMenu(string[] responses)
    {
        //How much should the next response be offset from the one before?
        float offsetFromTop = 0;

        //Begin making response boxes at the top of the menu and work down
        //Vector2 startPosition = responseMenu.rectTransform.

        for (int i = 0; i < responses.Length; i++)
        {
            int answerID;
            string resp = responses[i];
            (resp, answerID) = ParseForID(resp);
            Image res = GenerateResponse(resp, answerID);
            
            //Normalize the position and shape of the rectTransform
            res.rectTransform.localPosition = Vector3.zero;
            res.rectTransform.sizeDelta = new Vector2 (1, 1);
            Canvas.ForceUpdateCanvases();

            //Scale the size of the box according to how many lines of text it contains.
            float lineRatio = ((float) res.GetComponentInChildren<Text>().cachedTextGenerator.lineCount) / maxLinesPerWindow;
            float size = lineRatio * responseMenu.rect.size.y;

            res.rectTransform.SetInsetAndSizeFromParentEdge(RectTransform.Edge.Top, offsetFromTop, size);
            
            //Update the offset
            offsetFromTop += size;
        }
    }

    public Image GenerateResponse(string response, int id)
    {
        Image r = Instantiate(responseOption);
        r.rectTransform.SetParent(responseMenu);
        Text t = r.GetComponentInChildren<Text>();
        t.text = response;

        //Set the npcResponse in the responseOption
        //Add functions to the events
        ResponseOption res = r.GetComponent<ResponseOption>();
        res.OnSelect += Managers.Dialogue.Reply;
        res.npcResponseID = id;

        if (res.endConversation)
        {
            res.OnEndConversation += Managers.Dialogue.EndConversation;
        }
        return r;
    }

    public void ClearMenu()
    {
        ResponseOption[] options = GetComponentsInChildren<ResponseOption>();
        foreach (ResponseOption o in options)
        {
            Debug.Log("Destroy");
            Destroy(o.gameObject);
        }
    }

    private (string, int) ParseForID(string text)
    {
        //While reading a flag, this is true.
        bool readingFlag = false;
        //The string that is to be printed with all flags removed
        string printString = "", idText = "";
        char flagIndicator = '*';
        int id = -1;

        foreach(char c in text)
        {
            //Check if the char indicates a flag
            if (c.Equals(flagIndicator))
            {
                //If the terminating flag is found
                if (readingFlag)
                {
                    Debug.Log(idText);
                    idText = idText.TrimStart(' ', 'r');
                    id = Int32.Parse(idText);
                    idText = "";
                    readingFlag = false;
                }
                else
                    readingFlag = true;
                continue;
            }

            if (readingFlag)
            {
                idText += c;
                continue;
            }
            
            //If not reading a flag, then the char is added to the print string
            printString += c;
            
        }

        return (printString, id);
    }

    
}
