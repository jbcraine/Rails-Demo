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
        if (coll != null)
            coll.enabled = false;
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
            p.coll.enabled = true;
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

        foreach (Prop p in props)
        {
            p.enabled = false;
            p.coll.enabled = false;
        }
    }

    //This method subscribes to an event in the PokerView class. When it loads, nodes will be deactivated to prevent unwanted behavior.
    //When the UI is removed, then access to nodes is restored.
    public void OnPokerUISet(bool set)
    {
        SetEnabledNodes(set);
    }
}
