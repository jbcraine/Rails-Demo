using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//When an object with the Poker Game Starter is interacted with, then a Poker game is started 
//The prefabs for the player, ai, and Managers.Poker must be instantiated.

//
public class PokerGameStarter : Interactable
{
    public PokerGame gamePrefab;
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

    //This method would be better placed within a larger game Managers.Poker. Interact may call that method
    private void SetupPokerGame()
    {
        PokerGame game = Instantiate(gamePrefab);
        PlayerContestant player = Instantiate(playerPrefab);
        AIContestant ai = Instantiate(oppoonentPrefab);
        Deck gameDeck = Instantiate(deckPrefab);
        PokerController controller = Instantiate(PokerUIPrefab);

        controller.SetGameManager(game);
        controller.SetPlayer(player);

        List<AbstractContestant> contestants = new List<AbstractContestant>();
        contestants.Add(player);
        contestants.Add(ai);
        foreach (AbstractContestant c in contestants)
        {
            c.game = game;
        }
        game.SetContestants(contestants);
        game.SetDeck(gameDeck);

        Managers.PointAndClick.movement.SetPanels(ScrollDirection.None);
        
    }
}
