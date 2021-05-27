using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScrollMove : MonoBehaviour
{
    public GameObject playerPOV;
    Image leftPanel, rightPanel;
    public float scrollSpeed = 1.5f;
    public float sensitivity = 5.0f;
    public bool cursorOverLeftPanel, cursorOverRightPanel, cursorOverTopPanel, cursorOverBottomPanel;
    public bool useClampedMovement;
    public (float, float) clamps;

    private void Awake() {
        playerPOV = Managers.PointAndClick.playerRider.gameObject;
    }

    private void Start() {
    
    }
    private void Update() 
    {
        //Check if pointer is over leftPanel
        if (cursorOverLeftPanel)
        {
            ScrollLeft();
        }
        //Check if points is over rightPanel
        else if (cursorOverRightPanel)
        {
            ScrollRight();
        }    
    }
    public void ScrollLeft()
    {
        playerPOV.transform.Rotate(0, -scrollSpeed * sensitivity * Time.deltaTime, 0);
    }
    public void ScrollRight()
    {
        playerPOV.transform.Rotate(0, scrollSpeed * sensitivity * Time.deltaTime, 0);
    }

    public void IfCursorOverLeftPanel()
    {
        cursorOverLeftPanel = true;
        Debug.Log("L");
    }

    public void IfCursorOverRightPanel()
    {
        cursorOverRightPanel = true;
    }

    public void IfCursorOverTopPanel()
    {
        //This sucks but it works for now
        if(Managers.PointAndClick.currentViewNode.currentlyFocused)
            Managers.PointAndClick.currentViewNode.EndFocus();
        else
            Managers.PointAndClick.currentViewNode.Focus();
    }

    public void IfCursorOverBottomPanel()
    {
        if(!Managers.PointAndClick.currentViewNode.currentlyFocused)
            Managers.PointAndClick.currentViewNode.Focus();
        else
            Managers.PointAndClick.currentViewNode.EndFocus();
    }

    public void IfCursorNotOverLeftPanel()
    {
        cursorOverLeftPanel = false;
    }

    public void IfCursorNotOverRightPanel()
    {
        cursorOverRightPanel = false;
    }

    public void IfCursorNotOverTopPanel()
    {
        cursorOverTopPanel = false;
    }
    public void IfCursorNotOverBottomPanel()
    {
        cursorOverBottomPanel = false;
    }
}
