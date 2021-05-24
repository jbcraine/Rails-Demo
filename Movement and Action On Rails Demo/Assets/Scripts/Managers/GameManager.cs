using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager manager {get; private set;}
    //[HideInInspector]
    public Node currentNode;
    //[HideInInspector]
    public NodeViewer currentViewNode;
    public Node startingNode;
    public Rider playerRider;
    [HideInInspector]
    public Camera cam;
    public UIControls movement;
    public float playerHeight = 1.0f;
    public bool currentlyFocused = false;

    private void Awake() {
        manager = this;
        cam = Camera.main;
    }

    private void Start() {
        cam.transform.rotation = new Quaternion(0, 0, 0, 0);
        startingNode.Arrive();
        if (currentViewNode)
        {
            playerRider.transform.position = currentViewNode.standardOrientation.position;
            playerRider.transform.rotation = currentViewNode.standardOrientation.rotation;
        }
        else
        {
            playerRider.transform.position = currentNode.adjustedPosition;
            playerRider.transform.rotation = currentNode.rotation;
        }
    }
    private void Update() 
    {
        //If a Prop is currently active and the right mouse button is clicked, then return to the Prop's Location
        if (Input.GetMouseButtonDown(1) && currentNode.GetComponent<Prop>() != null)
        {
            currentNode.Leave();
            playerRider.StartMovingOnRail();
        } 
    }
}
