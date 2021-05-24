using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Location : Node
{
    public Location[] VIPNodes;
    public Prop[] props;
    public NPC[] npcs;
    

    // Start is called before the first frame update
    protected override void Awake()
    {
        //accessibleRails = new List<Rail>();
        //railsToDestinations = new Dictionary<Node, Rail>();
        base.Awake();
        coll = GetComponent<Collider>();
        view = GetComponent<NodeViewer>();
        PopulateRails();
    }

    protected override void Start() {
        base.Start();
    }

    public override void Arrive()
    {
        //if (view && GameManager.manager.currentlyFocused)
        base.Arrive();
        foreach(Prop p in props)
        {
            p.enabled = true;
        }

        foreach(NPC npc in npcs)
        {
            npc.Activate(true);
        }
    }

    public override void SetEnabledNodes(bool set)
    {
        base.SetEnabledNodes(set);
    }

    public override void Leave()
    {
        base.Leave();
        foreach (NPC npc in npcs)
        {
            npc.Activate(false);
        }
    }
}
