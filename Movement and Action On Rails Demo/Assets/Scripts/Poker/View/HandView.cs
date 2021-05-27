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
    public bool faceUp = false;
    public GameObject cardPrefab;
    public GameObject pokerUI;
    public GameObject cardHolder;

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

    private void Awake() {
        hand = GetComponent<Hand>();
        cardHolder = new GameObject("cardHolder");
        cardHolder.transform.position = start;
    }
    private void Start() {
        fetchedCards = new Dictionary<int, CardView>();
        cardHolder.transform.SetParent(pokerUI.transform);
        hand.CardAdded += hand_CardAdded;
    }

    void hand_CardAdded(object sender, CardEventArgs e)
    {
        float co = cardOffset * hand.numCards;
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
        cardCopy.transform.position = position;

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
