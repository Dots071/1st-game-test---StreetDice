using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI diceScoreText;
    public TextMeshProUGUI betText;
    public TextMeshProUGUI gameStageText;
    public TextMeshProUGUI toWinText;
    public TextMeshProUGUI toLoseText;

    public GameObject titleScreen;
    public GameObject gamePlayScreen;
    public Button startGameButton;
    public Button rollButton;
    public Button increaseBetButton;
    public Button lowerBetButton;

    private RollDice dice1;
    private RollDice dice2;

    private int cash = 100;
    private int scoreDiceOne = 6;
    private int scoreDiceTwo = 6;
    private int roundScore;
    private int roundStage;
    private int thePoint;

    public int currentBet = 1;
    public bool canRoll;

    // Start is called before the first frame update
    void Start()
    {
        UpdateCash(0);

        startGameButton.onClick.AddListener(TheComeOut);
        rollButton.onClick.AddListener(Roll);
        increaseBetButton.onClick.AddListener(increasBet);
        lowerBetButton.onClick.AddListener(lowerBet);

        dice1 = GameObject.Find("Dice 1").GetComponent<RollDice>();
        dice2 = GameObject.Find("Dice 2").GetComponent<RollDice>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Updates the player's cash according to money lost or won
    void UpdateCash(int cashChange)
    {
        cash += cashChange;
        cashText.text = "Cash: $" + cash;
    }

    // Get's the dice side facing up and updating it in the ScoreText
   public void UpdateDice(int diceNum, int diceSide)
    {
        if (diceNum == 1)
        {
            scoreDiceOne = diceSide;
        } else if (diceNum == 2)
        {
            scoreDiceTwo = diceSide;
        }

        diceScoreText.text = "Score: " + scoreDiceOne + " + " + scoreDiceTwo;     
    }

    // Phase one of the game - Not finished
    void TheComeOut()
    {
        roundStage = 1;
        titleScreen.SetActive(false);
        gamePlayScreen.SetActive(true);

        gameStageText.text = "The Comeout";
        toWinText.text = "To Win: 7, 11";
        toLoseText.text = "To Lose: 2, 3, 12";

        Debug.Log("The Comeout!!");

        canRoll = true;

    }

    void ThePoint(int point)
    {
        thePoint = point;
        roundStage = 2;

        gameStageText.text = "The Point";
        toWinText.text = "To Win: " + thePoint;
        toLoseText.text = "To Lose: 7";

        Debug.Log("The Point!!");
        canRoll = true;
    }

    // Checks to see that both dice stopped moving to roll
    void Roll()
    {
        if (!dice1.isThrown && !dice2.isThrown && canRoll)
        {
            dice1.Roll();
            dice2.Roll();
        }
       
    }

    // Get the total score for each round
    public void CalculateScore()
    {
        if (!dice1.isThrown && !dice2.isThrown)
        {
            canRoll = false;
            roundScore = scoreDiceOne + scoreDiceTwo;
            Debug.Log("You rolled " + roundScore);
            RoundCheck(roundScore);
        }
    }

    void RoundCheck(int roundScore)
    {
        if(roundStage == 1)
        {
            if (roundScore == 7 || roundScore == 11)
            {
                UpdateCash(currentBet);
                TheComeOut();
            }
            else if (roundScore == 2 || roundScore == 3 || roundScore == 12)
            {
                UpdateCash(-currentBet);
                TheComeOut();
            }
            else
            {
                ThePoint(roundScore);
            }
        }

        else if (roundStage == 2)
        {
            if (roundScore == 7)
            {
                UpdateCash(-currentBet);
                TheComeOut();
            }
            else if (roundScore == thePoint)
            {
                UpdateCash(currentBet);
                TheComeOut();
            }
            else
            {
                Debug.Log("Throw Again");
                canRoll = true;
            }
        }
    }

    void increasBet()
    {
        currentBet++;
        betText.text = currentBet.ToString();
    }

    void lowerBet()
    {
        currentBet --;
        betText.text = currentBet.ToString();
    }

}

