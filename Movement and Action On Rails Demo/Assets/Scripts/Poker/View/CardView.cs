using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A CardView will alwayts be associated with a Card object
public class CardView : MonoBehaviour
{
    public GameObject card { get; private set; }
    public bool isFaceUp { get; set; }

    public CardView(GameObject card)
    {
        this.card = card;
        isFaceUp = false;
    }
}
