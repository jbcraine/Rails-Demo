using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Hand))]
public class HandView : MonoBehaviour
{
    Hand hand;
    Dictionary<int, CardView> fetchedCards;

    public Vector2 start;
    public float cardOffset;
    public float paddingFromLeft;
    public bool faceUp = false;
    public GameObject cardPrefab;
    public PokerView pokerUI = null;

    //The element of the UI that contains the player's cards within the larger UI
    public RectTransform cardHolder;

    
     private void Awake() {
        hand = GetComponent<Hand>();
        
    }
    private void Start() {
        fetchedCards = new Dictionary<int, CardView>();
        pokerUI = FindObjectOfType<PokerView>();
        cardHolder = pokerUI.cardContainer;
        start = new Vector2(cardHolder.rect.min.x + paddingFromLeft, 0);
        //This stinks. Find a way to pass in the controller
        
        //cardHolder.transform.SetParent(pokerUI.transform);

        hand.CardAdded += hand_CardAdded;
    }

    //Get the rectTransform that the player's cards will be stored in within the UI
    public void OnUILoaded(object sender, UILoadedEventArgs e)
    {
        //cardHolder = e.container;
        //start = new Vector2 (0, 0);
    }

    public void Toggle (int card, bool isFaceUp)
    {
        fetchedCards[card].isFaceUp = isFaceUp;
    }

    public void hand_Clear(object sender)
    {
        Debug.Log("Clear");
        Clear();
        
    }
    public void Clear()
    {
        foreach(CardView view in fetchedCards.Values)
        {
            Destroy(view.card);
        }
        fetchedCards.Clear();
    }

   

    void hand_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * hand.numCards + paddingFromLeft;
        Vector2 temp = start + new Vector2(co, 0f);
        AddCard(temp, e.cardValue, e.cardSuit, hand.numCards);
    }

    private void Update() {
        //ShowCards();
    }

    public void ShowCards()
    {
        int cardCount = 0;
        if (hand.numCards > 0)
        {
            foreach(Card card in hand.cards)
            {
                float co = cardOffset * cardCount;
                Vector2 temp = start + new Vector2(co, 0f);
                AddCard(temp, card.value, card.suit, cardCount);

                cardCount++;
            }
        }
    }

    //Add a visual card to the hand 
    void AddCard(Vector3 position, CardValue value, CardSuit suit, int positionIndex)
    {
        //Determine the index of the cardface in the CardFace array from the value and suit

        GameObject cardCopy = Instantiate(cardPrefab, cardHolder.transform);

        //The card will be placed into the cardHolder
        cardCopy.transform.SetParent(cardHolder.transform);
        cardCopy.transform.localPosition = position;

        int cardIndex = GetCardIndex(value, suit);

        CardFace cardModel = cardCopy.GetComponent<CardFace>();
        cardModel.cardIndex = cardIndex;    
        cardModel.ToggleFace(faceUp);

        //Cannot use new
        fetchedCards.Add(cardIndex, cardCopy.GetComponent<CardView>());
    }

    int GetCardIndex(CardValue value, CardSuit suit)
    {
        return ((int) value * 4) + ((int) suit);
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(start, new Vector3(50, 75, 1));
    }

}
