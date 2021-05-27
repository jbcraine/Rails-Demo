using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A VIP Node is considered to be a Node that is given the special status by a Location.
// A VIP Node may access a Node that normally requires Focus without the need to be Focused
public class VIPNodeCondition : Prerequisite
{
    private Location[] VIPNodes;

    protected void Start()
    {
        VIPNodes = GetComponent<Location>().VIPNodes;
    }

    private bool IsCurrentNodeVIP()
    {
        foreach (Location l in VIPNodes)
        {
            if (Managers.PointAndClick.currentNode.Equals(l))
                return true;
        }
        return false;
    }
    public override bool complete
    {
        get {return IsCurrentNodeVIP();}
    }
}
