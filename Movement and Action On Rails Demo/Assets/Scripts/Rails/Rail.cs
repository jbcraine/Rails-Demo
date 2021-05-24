using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Rail : MonoBehaviour
{
    //Rails conjoin two Nodes...
    public Node pointA, pointB;
    //With an arbitrary number of Medium Nodes inbetween
    public MediumNode[] mediumNodes;
    [HideInInspector]
    public List<Node> fullRail;

    public int railLength
    {
        get {return fullRail.Count;}
    }

    private void Awake() {
        fullRail = new List<Node>();
        BuildRail();
    }

    private void BuildRail()
    {
        fullRail.Add(pointA);
        foreach(Node node in mediumNodes)
            fullRail.Add(node);
        fullRail.Add(pointB);
    }

    public Node GetNodeAtPosition(int index)
    {
        return fullRail[index];
    }

    public Vector3 GetPositionAtNode(int node)
    {
        return fullRail[node].adjustedPosition;
    }

    public Quaternion GetRotationAtNode(int node)
    {
        return fullRail[node].rotation;
    }

    public bool UseCameraPositionOfNode(int node)
    {
        return !fullRail[node].ignoreOrientation;
    }

    public Node GetDestinationFromNode(Node startNode)
    {
        if (startNode.Equals(pointA))
            return pointB;
        else if (startNode.Equals(pointB))
            return pointA;
        return null;
    }

    
    private void OnDrawGizmos() {
        //Handles.color = Color.green;
        for (int i = 0; i < fullRail.Count - 1; ++i)
        {
            Handles.DrawDottedLine(GetPositionAtNode(i), GetPositionAtNode(i + 1), 3.5f);
        }

    }
}
