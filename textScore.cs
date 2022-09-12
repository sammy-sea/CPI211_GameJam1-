// textScore.cs
// Description: This script displays the current level the player is on
//              through the UI


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class textScore : MonoBehaviour
{
    BallMovement ballMovement; // uses script used in the player object
    [SerializeField] GameObject player; // player object
    public Text scoreText; // Text that displays the information to the player
    private float totalCoins;

    //Awake is called before any Start() functions
    void Awake()
    {
        ballMovement = player.GetComponent<BallMovement>(); // script that holds the coin count in the player object
    }

    void Start()
    {
        totalCoins = GameObject.FindGameObjectsWithTag("Coin").Length;
    }

    void Update()
    {
        scoreText.text = "Flags: " + ballMovement.coinCount.ToString();// Text that is displayed: "Coins: [coin count]"
    }
}
