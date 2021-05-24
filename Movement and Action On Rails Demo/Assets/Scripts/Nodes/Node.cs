using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public  abstract class Node : MonoBehaviour
{
    [SerializeField]
    protected Transform standardPosition;
    public Vector3 adjustedPosition {get; protected set;}

    //Rails provide links to all Locations and Props that are accessible from this Node
    public List<Rail> accessibleRails;
    //Connect each Rail with the Node it leads to from this Node
    public Dictionary<Node, Rail> railsToDestinations = new Dictionary<Node, Rail>();

    [HideInInspector]
    public Collider coll;
    protected NodeViewer view;
    public bool hasFocus;
    [HideInInspector]
    public Metarequisite metarequisite {get; private set;}
    //TODO: Each Node can contian a variable saying how quickly the Rider should move

    protected virtual void Awake() {
        adjustedPosition = new Vector3(standardPosition.position.x, standardPosition.position.y + GameManager.manager.playerHeight, 
            standardPosition.position.z);
    }

    protected virtual void Start() {
        metarequisite = GetComponent<Metarequisite>();
    }

    public Vector3 position
    {
        get {return standardPosition.position;}
    }

    public Quaternion rotation
    {
        get {if (!view) {return Camera.main.transform.rotation;} return view.standardOrientation.rotation;}
    }

    public bool ignoreOrientation
    {
        get {if (!view) {return true;} 
            return view.ignoreStandardOrientation;}
    }

    public virtual void Arrive()
    {
        /*Check all the prerequisites required for accessing this Node. Make sure they are all met.
        if (prerequisites != null)
        {
            foreach(Prerequisite p in prerequisites)
            {
                if (!p.complete)
                    return;
            }
        }
        */
        if (GameManager.manager.currentNode != null)
            GameManager.manager.currentNode.Leave();

        //If this Node has a view associated with it, then enable the component
        if (view)
        {
            view.enabled = true;
            GameManager.manager.currentViewNode = view;
        }

        GameManager.manager.currentNode = this;
        if (coll)
            coll.enabled = false;
        SetEnabledNodes(true);
    }

    public virtual void Leave()
    {
        SetEnabledNodes(false);
        //Disable the view component when leaving the node
        if (view)
        {
            view.enabled = false;
            GameManager.manager.currentViewNode = null;
            view.LeaveView();
        }
    }

    public virtual void SetEnabledNodes(bool set)
    {
        if (railsToDestinations.Count <= 0)
            return;

        foreach(KeyValuePair<Node, Rail> kvp in railsToDestinations)
        {
            kvp.Key.coll.enabled = set;
        }
    }

    //When a Node's collider is clicked, then the Player moves towards that Node
    protected void OnMouseDown() 
    {
        //Get the Rail that leads from this Node to the currentNode
        Rail hopOnRail = railsToDestinations[GameManager.manager.currentNode];
        GameManager.manager.playerRider.currentRail = hopOnRail;
        GameManager.manager.playerRider.StartMovingOnRail();
    }

    //Populate the dictionary
    protected void PopulateRails()
    {
        foreach(Rail rail in accessibleRails)
        {
            railsToDestinations.Add(rail.GetDestinationFromNode(this), rail);
        }
    }
}
