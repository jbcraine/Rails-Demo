using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

//NodeViewer provides settings to control the viewing of Nodes
public abstract class NodeViewer : MonoBehaviour
{
    //The orientation to adjust the Camera when viewing the Node
    public Transform focusOrientation;
    //The orientation to adjust the Camera to when done viewing the Node
    public Transform standardOrientation;
    
    [SerializeField]
    //What direction does the POV need to be turned at to view the ViewNode (Can only be a single direciton)
    protected ScrollDirection _startFocusDirection;

    //What directions become available while focused? (Can be multiple directions, and must be at least one to leave Focus)
    [SerializeField]
    protected ScrollDirection _midFocusDirections;

    //Does the Node have a focus view to be focused on? If so, allow it to be focused. If not, then disable the option to focus.
    public bool hasFocusView;
    //If for whatever reason the standard orientation should be ignored, set this state to true
    public bool ignoreStandardOrientation = false;

    //Is the Location currently being focused on?    
    public bool currentlyFocused = false;

    [SerializeField]
    protected (float, float) clampRotationWhileFocused;
    public bool useClampWhenFocused {get; protected set;}
  
    //public event OnFocusEventHandler focusChange;

    public (float, float) clamps
    {
        get {return clampRotationWhileFocused;}
    }

    protected void Awake() {
        
    }

    protected virtual void Start() {
        standardOrientation.position = GetComponent<Node>().adjustedPosition;
        //Automatically disable this component when starting. Enable it again when necessary
        this.enabled = false;
    }

    protected virtual void Update() {
        //If there is no focus view, then there is nothing to do
        if (!hasFocusView)
            return;
    }

    //To view the Node, adjust the orientation of the main camera
    public void Focus()
    {
        Managers.PointAndClick.playerRider.transform.DOMove(focusOrientation.position, 0.8f);
        Managers.PointAndClick.playerRider.transform.DORotate(focusOrientation.rotation.eulerAngles, 0.8f);

        Managers.PointAndClick.movement.SetPanels(_midFocusDirections);
        currentlyFocused = true;
        //focusChange(true);
        Managers.PointAndClick.currentlyFocused = true;
    }

    public void EndFocus()
    {
        Managers.PointAndClick.playerRider.transform.DOMove(standardOrientation.position, 0.8f);
        Managers.PointAndClick.playerRider.transform.DORotate(standardOrientation.rotation.eulerAngles, 0.8f);
        
        Managers.PointAndClick.movement.SetPanels(ScrollDirection.Left | ScrollDirection.Right | _startFocusDirection);
        currentlyFocused = false;
        //focusChange(false);
        Managers.PointAndClick.currentlyFocused = false;
    }

    //Called when leaving a Node to reset attributes 
    public virtual void LeaveView()
    {
        currentlyFocused = false;
    }

    public void SetMoveUI()
    {

    }
}
//If look down, check if the currentnode has a viewNode and if it is active, and focus on it if true
