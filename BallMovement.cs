// BallMovement.cs
// Description: This script takes inputs from the user to control the movement of
//              the ball


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BallMovement : MonoBehaviour
{
    // Public variables
    public GameObject flags;
    public Transform ballOrientation; // uses another object to keep track of orientation for forward movement
    public float movementSpeed; // speed that the ball moves
    public float brakeForce; // force of the brakes (higher val = faster brake)
    public float maxSpeed; // speed cap
    public float jumpForce; // force applied to the ball to make it jump
    public float playerHeight; // height of the physical ball (used to calculate raycast)
    public float coinCount; // coins collected (this variable is used by other scripts)


    // Private variables
    private Rigidbody rb; // rigidbody of the physical ball
    private Vector3 initialPosition; // initial position of the ball in the level (spawn point)
    private float forward; // 'W' input
    private bool brake; // 'S' input
    private bool spaceInput; // 'Space' input
    private bool isGrounded; // whether the raycast is touching an object or not


    void Start()
    {
        rb = GetComponent<Rigidbody>(); // get physical ball rigidbody
        coinCount = 0f; // coin count resets after scene transition
        initialPosition = transform.position; // spawn position
    }


    void Update()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.1f); // if raycast collides with any object, player is grounded            
        PlayerInput(); // player inputs
        MovePlayer(); // player physics and movement in game
        ResetPosition(); // respawn when player drops below certain height

        if (rb.velocity.magnitude > maxSpeed) // prevents player from infinitely accelerating
            rb.velocity = rb.velocity.normalized * maxSpeed; // cap speed at maxSpeed
    }

    /// stores values of player inputs in variables. also calls cheat functions for naviagting through levels
    void PlayerInput()
    {
        // forward input converted from a bool value to a float
        if (Input.GetKey(KeyCode.W))
            forward = 1;
        else
            forward = 0;

        brake = Input.GetKey(KeyCode.S);
        spaceInput = Input.GetKeyDown(KeyCode.Space);

        
        if (Input.GetKeyDown(KeyCode.N)) // call skip scene function
            NextLevel();
        if (Input.GetKeyDown(KeyCode.P)) // call previous scene function
            PreviousLevel();
        
    }

    /// moves player using physics
    void MovePlayer()
    {
        if (brake == false) // if brake is not held, player can move the ball forward
            rb.velocity += ballOrientation.forward * forward * movementSpeed * Time.deltaTime;
        else if (brake == true && isGrounded == true) // if brake is held and player is on the ground, decelerate to a stop
            rb.velocity -= rb.velocity * brakeForce * Time.deltaTime;

        if (spaceInput == true && isGrounded == true) // if player is on the ground and space is pressed, call jump function
            PlayerJump();
    }

    /// adds velocity on the Y axis to make the player jump
    void PlayerJump()
    {
        rb.velocity += new Vector3(0f, jumpForce, 0f);
    }

    /// trigger collision detector
    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Coin") // if player collides with a coin, delete the coin and increment coin count by 1
        {
            Destroy(other.gameObject); // destroy the coin object
            coinCount += 1; // increment coin count
        }
        if (other.gameObject.tag == "Goal") // if player collides with a coin, delete the coin and increment coin count by 1
        {
            Destroy(other.gameObject); // destroy the coin object
            NextLevel();
        }
    }

    // collision detector
    void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Enemy") // check if player collides with enemy
        {
            if(other.transform.position.y < transform.position.y - playerHeight * 0.01f)
            {
                Vector3 enemyPosition = other.transform.position;
                Destroy(other.gameObject);
                Instantiate(flags, enemyPosition, Quaternion.identity);
            }
        }
    }

    /// resets player position and velocity if player falls out of bounds
    private void ResetPosition()
    {
        if (transform.position.y < -10) // if player falls below -10 on Y axis
        {
            rb.velocity += -rb.velocity; // set velocity to 0
            transform.position = initialPosition; // set initial position to 0
        }
    }

    /// skips to the next scene in the heiarchy
    private void NextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1; // next scene index = current scene index + 1
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings); // check next scene index is within bounds
            SceneManager.LoadScene(nextSceneIndex); // load next scene
    }

    /// goes to the previous scene in the heiarchy
    private void PreviousLevel()
    {
        int previousSceneIndex = SceneManager.GetActiveScene().buildIndex - 1; // previous scene index = current scene index - 1
        if (previousSceneIndex >= 0) // check previous scene is within bounds
            SceneManager.LoadScene(previousSceneIndex); // load previous scene
    }

}
