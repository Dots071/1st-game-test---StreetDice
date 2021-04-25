using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideDetector : MonoBehaviour
{
    public int diceNum;
    public int diceSide;

    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    // Detects which side of the dice has landed on the ground
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DiceFace"))
        {
            diceSide = other.GetComponent<DiceResults>().oppositeNumber;
            diceNum = other.GetComponent<DiceResults>().diceNum;
            Debug.Log("Dice " + diceNum + " rolled the number " + diceSide);

            gameManager.UpdateDice(diceNum, diceSide);
        }
    }

}
