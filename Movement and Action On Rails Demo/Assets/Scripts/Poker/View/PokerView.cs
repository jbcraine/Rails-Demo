using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Split the Poker controller into controller and view, with view items going in here. Communicate using events.
public class PokerView : MonoBehaviour
{
    public Text callAndRaiseText, availableMoney, callAndCheckButtonText, currentBetText, currentPotText;
    public Text winnerText, winningHandText;
    public Text contestantEliminatedText;
    public RectTransform cardContainer;
    HandView handView;
    public event UILoadedEventHandler UILoaded;

    private void Awake() {
        Managers.Poker.BetChanged += OnBetChanged;
        Managers.Poker.PotChanged += OnPotChanged;
        Managers.Poker.ContestantEliminated += OnContestantEliminated;
        Managers.Poker.PotWon += OnPotWon;
        Managers.Poker.RoundEnded += OnRoundEnded;
        Managers.Poker.CalledAllContestants += OnAllContestantsCalled;
        Managers.Poker.MatchEnded += OnMatchEnded;
        
        FindObjectOfType<PlayerContestant>().MoneyChanged += OnMoneyChanged;

        handView = FindObjectOfType<HandView>();
        UILoaded += handView.OnUILoaded;
    }

    // Start is called before the first frame update
    void Start()
    {
        UILoaded(this, new UILoadedEventArgs(cardContainer));
    }


    void OnMoneyChanged (object sender, MoneyEventArgs e)
    {
        availableMoney.text = "$" + e.money;
    }

    public void OnMoneyValue(float value)
    {
        callAndRaiseText.text = "$" + value;
    }

    void OnBetChanged(object sender, BetEventArgs e)
    {
        callAndCheckButtonText.text = "$" + e.currentBet.ToString();
        callAndCheckButtonText.text = "Call";
        currentBetText.text = "Current Bet: $" + e.currentBet.ToString();
    }

    void OnPotChanged(object sender, PotChangedEventArgs p)
    {
        currentPotText.text = "Current Pot: $" + p._pot.ToString();
    }

    void OnRoundStarted()
    {

    }

    void OnRoundEnded(object sender)
    {
        winnerText.gameObject.SetActive(false);
        winningHandText.gameObject.SetActive(false);
    }

    void OnPotWon(object sender, WonPotEventArgs e)
    {
        winnerText.text = e.winnerName + " Wins the Round!";
        winnerText.gameObject.SetActive(true);

        if (e.showDownHappened)
        {
            if (e.showDownDrawed)
            {
                winnerText.text = "Draw!";
                winningHandText.text = e.handName;
            }
            else
            {
                winningHandText.text = e.handName;
            }

            winningHandText.gameObject.SetActive(true);
        }
    }

    void OnContestantEliminated(object sender, ContestantEliminatedEventArgs e)
    {
        winningHandText.gameObject.SetActive(false);
        contestantEliminatedText.text = e.contestantName + " has been eliminated from play.";
    }

    void OnAllContestantsCalled(object sender)
    {
        callAndCheckButtonText.text = "Check";
    }

    void OnMatchEnded(object sender, MatchEndEventArgs e)
    {
        winnerText.text = e.winnerName + "has Won the Game!";
    }
}
