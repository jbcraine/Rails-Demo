using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PokerController : MonoBehaviour
{
    [SerializeField] private PlayerContestant player;
    private PokerGame game;
    public Button checkButton, raiseButton, foldButton;
    public Button startGameButton, startRoundButton;
    //The maximum value will need to be changed as the player's money changes
    public Slider moneySlider;
    private int currentMoney;
    private int currentBet;
    private int currentCall;
    private int currentPot;
    //This will be the max value of the raise slider
    private int roundStartingMoney;
    
    private void Start() {
        game = FindObjectOfType<PokerGame>();

        if (game != null)
        {
            player.MoneyChanged += OnMoneyChanged;
            game.BetChanged += OnBetChanged;
            game.PotChanged += OnPotChanged;
            game.RoundStarted += OnRoundStarted;
            game.ContestantEliminated += OnContestantEliminated;
            game.PotWon += OnPotWon;
            game.RoundEnded += OnRoundEnded;
            game.CalledAllContestants += OnAllContestantsCalled;
            game.MatchEnded += OnMatchEnded;
            game.KillGame += OnKillGame;
        }
    }

    //Activate the UI when it is the player's turn
    public void ActivateUI()
    {
        checkButton.interactable = true;
        raiseButton.interactable = true;
        foldButton.interactable = true;
        moneySlider.interactable = true;
    }

    //Deactivate the UI when the player finishes their turn
    public void DeactivateUI() 
    {
        checkButton.interactable = false;
        raiseButton.interactable = false;
        foldButton.interactable = false;
        moneySlider.interactable = false;
    }

    //Assign a PokerManager to this controller
    public void SetGameManager(PokerGame manager)
    {
        game = manager;
    }

    //Assign a PlayerContestant to this controller
    public void SetPlayer(PlayerContestant player)
    {
        this.player = player;
    }

    public void OnCall()
    {
        IPokerCommand command = null;
        if (game.isPlayerTurn)
        {
            if((player.money + player.betMoney) >= currentCall)
                command = new CallCommand(player);
            else
                command = new CallCommand(player);
            DeactivateUI();
        }
        
        command.Execute();
    }

    public void OnFold()
    {
        IPokerCommand command = null;
        if (game.isPlayerTurn)
        {
            command = new FoldCommand(player);
            DeactivateUI();
        }

        command.Execute();
    }

    public void OnRaise()
    {
        IPokerCommand command = null;
        if (game.isPlayerTurn)
        {
            command = new RaiseCommand(player, currentBet);
            DeactivateUI();
        }

        command.Execute();
    }

    public void OnStartGame()
    {
        game.StartMatch();
        startGameButton.interactable = false;
    }

    public void OnStartRound()
    {
        game.StartNextRound();
        startRoundButton.interactable = false;
    }

    public void OnMoneyValue(float value)
    {
        //Value should adjust in increments of 10
        float adjustedValue = (value / 10) * 10;

        //moneySlider.value = adjustedValue;
        //currentBet = (int) adjustedValue;
        currentBet = (int) value;
        
    }


    void OnMoneyChanged(object sender, MoneyEventArgs e)
    {
        //Debug.Log(e.money);
        currentMoney = e.money;
    }

    void OnBetChanged(object sender, BetEventArgs b)
    {
        //Debug.Log(b.currentBet);
        currentCall = b.currentBet;
        moneySlider.minValue = currentCall;
    }

    void OnPotChanged(object sender, PotChangedEventArgs p)
    {
        currentPot = p._pot;
    }

    void OnRoundStarted(object sender, RoundStartedEventArgs r)
    {
        roundStartingMoney = r._startingMoney;
        moneySlider.maxValue = roundStartingMoney;
    }

    void OnPotWon(object sender, WonPotEventArgs w)
    {
        //handName will be null if all by one contestant folds
    }

    void OnContestantEliminated (object sender, ContestantEliminatedEventArgs c)
    {
        
    }

    void OnRoundEnded (object sender) 
    {
        startRoundButton.interactable = true;
    }

    void OnAllContestantsCalled (object sender)
    {
    
    }

    void OnMatchEnded (object sender, MatchEndEventArgs m)
    {
        startGameButton.interactable = true;
        startRoundButton.interactable = false;
    }

    public void TeardownController()
    {

    }

    public void OnKillGame()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Destroy(this.gameObject);
    }
}
