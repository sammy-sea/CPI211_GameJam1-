// ObjectMovement.cs
// Description: This script allows an object to move back and forth on any axis


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    
    public float bobSpeed; // speed that the object moves up and down
    public bool moveOnX;
    public bool moveOnY;
    public bool moveOnZ;
    public float maxX;
    public float maxY;
    public float maxZ;

    // initial position in the world
    private float initialX;
    private float initialY;
    private float initialZ;

    private float newX;
    private float newY;
    private float newZ;

    void Start()
    {
        // assign values to initial position
        initialX = transform.position.x;
        initialY = transform.position.y;
        initialZ = transform.position.z; 
    }

    // Update is called once per frame
    void Update()
    {   
        if (moveOnX == true)
            newX = UpdateXPosition();
        else
            newX = 0;
            
        if (moveOnY == true)
            newY = UpdateYPosition();
        else
            newY = 0;

        if (moveOnZ == true)
            newZ = UpdateZPosition();
        else
            newZ = 0;

        UpdatePosition(newX, newY, newZ);
    }

    void UpdatePosition(float x, float y, float z)
    {
        transform.position = new Vector3(initialX + x, initialY + y, initialZ + z);  // translate the object to the new position
    }

    private float UpdateXPosition()
    {
        float x = Mathf.PingPong(Time.time * bobSpeed, 1) * maxX; // calculates new X position along the specified length of the X axis
        return x;
    }

    private float UpdateYPosition()
    {
        float y = Mathf.PingPong(Time.time * bobSpeed, 1) * maxY; // calculates new Y position along the specified length of the Y axis
        return y;
    }

    private float UpdateZPosition()
    {
        float z = Mathf.PingPong(Time.time * bobSpeed, 1) * maxZ; // calculates new Z position along the specified length of the Z axis
        return z;
    }
}
