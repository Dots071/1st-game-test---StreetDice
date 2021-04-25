using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollDice : MonoBehaviour
{
    private Rigidbody diceRb;
    private GameManager gameManager;
    public bool isThrown;
    private Vector3 initPosition;

    public float throwSpeed = 50;

    // Start is called before the first frame update
    void Start()
    {
        diceRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        isThrown = false;
        diceRb.useGravity = false;

        initPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {


        if (isThrown)
            {
            if (diceRb.IsSleeping())
            {
                isThrown = false;
                diceRb.useGravity = false;
                Debug.Log("Dice Landed!!!");
                gameManager.CalculateScore();
            }
           
        }

    }

    // Rolls the dice via a random force and starting from a random rotation
    public void Roll()
    {
        transform.position = initPosition;
        transform.rotation = RandomRotation();

        isThrown = true;
        diceRb.useGravity = true;

        diceRb.AddForce(RandomForce() * throwSpeed, ForceMode.Impulse);
        diceRb.AddTorque(Vector3.forward * throwSpeed, ForceMode.Force);
    }

 
    private Quaternion RandomRotation()
    {
        return new Quaternion(Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180), Random.Range(0, 180));
    }

    private Vector3 RandomForce()
    {
        return new Vector3(Random.Range(0.25f, 1), 0, Random.Range(0.25f, 1));
    }
}
