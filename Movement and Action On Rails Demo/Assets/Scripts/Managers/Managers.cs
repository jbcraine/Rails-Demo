using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (PointAndClickManager))]
[RequireComponent (typeof (PokerManager))]
[RequireComponent (typeof (DialogueManager))]
public class Managers : MonoBehaviour
{
    public static PointAndClickManager PointAndClick {get; private set;}
    public static PokerManager Poker {get; private set;}
    public static DialogueManager Dialogue {get; private set;}

    private void Awake() {
        //PointAndClick = GetComponent<PointAndClickManager>();
        //Poker = GetComponent<PokerManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
}
