// CoinBehavior.cs
// Description: This script animates the coin to make it clear to the player
//              that the coins are collectable. The movement of the coin is
//              a slow and steady vertical bouncing movement as well as a
//              slow spinning movement. (This script is also used to animate
//              the end crystal)


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehavior : MonoBehaviour
{
    public float bobSpeed; // vertial speed of the coin
    public float maxHeight; // max height the coin moves vertically
    public float rotationSpeed; // speed at which the coin spins

    // initial position of the coin
    private float initialX;
    private float initialY;
    private float initialZ;

    void Start()
    {
        // assign values to the initial position of the coin
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z;   
    }

    void Update()
    {
        float y = Mathf.PingPong(Time.time * bobSpeed, 1) * maxHeight; // Y position gets updated everyframe to a point along Y axis
        transform.position = new Vector3(initialX, y + initialY, initialZ); // new position of the coin (only changes the Y value)    
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * 100); // rotates the coin at a constant speed
    }
}
