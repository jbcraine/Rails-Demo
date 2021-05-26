using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When an object with the Poker Game Starter is interacted with, then a Poker game is started 
//The prefabs for the player, ai, and manager must be instantiated.

//
public class PokerGameStarter : Interactable
{
    public PokerManager managerPrefab;
    public PlayerContestant playerPrefab;
    public AIContestant oppoonentPrefab;
    public Deck deckPrefab;
    public PokerController PokerUIPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public override void Interact()
    {
        SetupPokerGame();
    }

    //This method would be better placed within a larger game manager. Interact may call that method
    private void SetupPokerGame()
    {
        PokerManager manager = Instantiate(managerPrefab);
        PlayerContestant player = Instantiate(playerPrefab);
        AIContestant ai = Instantiate(oppoonentPrefab);
        Deck gameDeck = Instantiate(deckPrefab);
        PokerController controller = Instantiate(PokerUIPrefab);

        controller.SetGameManager(manager);
        controller.SetPlayer(player);

        List<AbstractContestant> contestants = new List<AbstractContestant>();
        contestants.Add(player);
        contestants.Add(ai);
        foreach (AbstractContestant c in contestants)
        {
            c.game = manager;
        }
        manager.SetContestants(contestants);
        manager.SetDeck(gameDeck);
    }
}
