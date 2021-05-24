using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LocationViewer : NodeViewer
{
    //The medium exists inbetween the Node and the Player. When the medium is looked at, the option to focus is given.
    [SerializeField]
    private Transform _medium;

    //Only enable the option to view the Node when the Camera is facing it (via Dot product)
    public bool isCameraFacingNode;

    //Should the player automatically focus on the node when arriving at it?
    public bool automaticallyFocus = false;

    //Is the directional panel for focusing on the Node enabled?
    protected bool _startFocusDirectionEnabled = false;
    
    protected override void Start()
    {
        base.Start();
    }
    
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (!currentlyFocused)
        {
            SetFocusScroll();
        }
        else
        {

        }
    }

    //Check if the camera is facing the medium to set the scroller to focus
    protected void SetFocusScroll()
    {
        //If the camera is facing the location, then enable the option to 
        float dot = Vector3.Dot(GameManager.manager.cam.transform.forward, (_medium.position - GameManager.manager.cam.transform.position).normalized);

        if (dot >= 0.95f && !isCameraFacingNode)
        {
            Debug.Log("Facing Node");
            isCameraFacingNode = true;
        }
        else if (dot < 0.95f && isCameraFacingNode)
        {
            Debug.Log("Not Facing Node");
            isCameraFacingNode = false;
        }

        //When the camera is facing the location, then enable the focus direction
        if (isCameraFacingNode && !_startFocusDirectionEnabled)
        {
            GameManager.manager.movement.SetExclusivePanel(_startFocusDirection, true);
            _startFocusDirectionEnabled = true;
        }
        else if (!isCameraFacingNode && _startFocusDirectionEnabled)
        {
            GameManager.manager.movement.SetExclusivePanel(_startFocusDirection, false);
            _startFocusDirectionEnabled = false;
         }
    }

    public override void LeaveView()
    {
        base.LeaveView();
        isCameraFacingNode = false;
        _startFocusDirectionEnabled = false;
    }
}
