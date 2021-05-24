using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

//The Rider moves from one end of a Rail to the other
public class Rider : MonoBehaviour
{
    [HideInInspector]
    public GameObject railRider {get; private set;}
    public Rail currentRail;

    private Direction _direction;

    //How far along on the Rail has the Rider come so far? Counts in terms of Nodes
    private int progress;
    private int destination;
    bool currentlyMoving = false;
    bool completedRail = true;
    Vector3 targetedPosition;

    public enum Direction
    {
        Forwards,
        Backwards
    }

    private void Awake() {
        railRider = this.gameObject;
    }
    
    private void Update() {
        if (!currentRail)
            return;

        if (!currentlyMoving)
            return;

        if (targetedPosition == GameManager.manager.playerRider.transform.position)
        {
            ArriveAtNode(currentRail.GetNodeAtPosition(progress));
            Move();
            GameManager.manager.movement.SetPanels(ScrollDirection.Left | ScrollDirection.Right);
        }
    }

    
    public void StartMovingOnRail()
    {
        //Start by determining which side of the Rail the Rider is on..
        if (GameManager.manager.currentNode.Equals(currentRail.pointA))
        {
            //A ---> B
            _direction = Direction.Forwards;
            progress = 0;
            destination = currentRail.railLength - 1;
        }
        else if (GameManager.manager.currentNode.Equals(currentRail.pointB))
        {
            //B ---> A
            _direction = Direction.Backwards;
            progress = currentRail.railLength - 1;
            destination = 0; 
        }

        //Check if the destination can be accessed
        //NEEDS REFACTORING IN NEWER VERSION
        if (currentRail.GetNodeAtPosition(destination).metarequisite != null && 
            !currentRail.GetNodeAtPosition(destination).metarequisite.complete)
                return;

        //And move in the correct direction   
        currentlyMoving = true;
        completedRail = false;
        //When moving on a Rail, nothing is being focused on
        GameManager.manager.currentlyFocused = false;
        GameManager.manager.movement.SetPanels(ScrollDirection.None);
        Move();
    }

    private void Move()
    {
        //If the end of the rail is reached, then sign off that the rider is done moving
        if (progress == destination)
        {
            completedRail = true;
            currentlyMoving = false;
            return;
        }

        LeaveFromNode(currentRail.GetNodeAtPosition(progress));

        if (_direction == Direction.Forwards)
        {
            targetedPosition = currentRail.GetPositionAtNode(progress + 1);
            GameManager.manager.playerRider.transform.DOMove(targetedPosition, 2.0f);

            if (currentRail.UseCameraPositionOfNode(progress + 1))
            {
                Quaternion nextRotation = currentRail.GetRotationAtNode(progress + 1);
                GameManager.manager.playerRider.transform.DORotate(nextRotation.eulerAngles, 2.0f);
            }

            progress += 1;
        }
        else if (_direction == Direction.Backwards)
        {
            targetedPosition = currentRail.GetPositionAtNode(progress - 1);
            GameManager.manager.playerRider.transform.DOMove(targetedPosition, 2.0f);

            if (currentRail.UseCameraPositionOfNode(progress - 1))
            {
                Quaternion nextRotation = currentRail.GetRotationAtNode(progress - 1);
                GameManager.manager.playerRider.transform.DORotate(nextRotation.eulerAngles, 2.0f);
            }

            progress -= 1;
        }
    }

    private void ArriveAtNode(Node node)
    {
        node.Arrive();
    }

    private void LeaveFromNode(Node node)
    {
        node.Leave();
    }
}
//Note: What to do about completedRail? It doesn't seem to be used for anything that currentlyMoving doesn't already do
//TODO: Create an enum for MovementStyle. Have options for Lerping and for DOTween.
//Add logic to Rail for Lerping and add logic to account for it.
//Move should be a generic function, that based on the MovementStyle, selects a function to call.