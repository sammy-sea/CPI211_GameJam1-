// BallOrientation.cs
// Description: This script uses an empty object to store the player ball's
//              orientation. Using this orientation allows the ball to roll
//              and have it's own orientation but also make it move forward
//              relative to the camera


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallOrientation : MonoBehaviour
{
    public Transform targetBall; // Physical player ball
    public float rotationSpeed; // Speed that the player can turn the ball

    private float xInput; // 'A' and 'D' input

    void Update()
    {
        transform.position = targetBall.transform.position; // Position is fixed to the physical ball
        xInput = Input.GetAxisRaw("Horizontal");
        rotateBall();
    }

    /// Turns the ball without changing the physical ball's orientation
    void rotateBall()
    {
        transform.Rotate(Vector3.up * xInput * rotationSpeed * Time.deltaTime * 100); // rotates about the Y axis
    }
}
