using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

    float ballSpeed;

    Vector3 playerPos;
    Vector3 spawnedPos;
    float angle;

	// Use this for initialization
	void Start () {
        spawnedPos = transform.position;
        CalculateAngle();
	}
	
	// Update is called once per frame
	void Update () {
        MoveBall();
	}

    void SetParentPos(Vector3 parentPos)
    {
        playerPos = parentPos;
    }

    void CalculateAngle()
    {
        // get the ball's pos
        // get the player's current pos

        // the ball's pos should be set
        Debug.Log("Ball's pos for " + gameObject.name + ": " + playerPos);

        // the player's pos should be set already
        Debug.Log("Player's pos for " + gameObject.name + ": " + playerPos);

        // get the angle of player's pos to ball's pos
        
        // opp = height = ballY - playerY
        // adj = width = ballX - playerX        
        // hyp = distance = sqrt((ballX - playerX)^2 + (ballY - playerY)^2)
        // tan(theta) = opp/adj
        // theta = tan^-1(opp/adj)

        float height = spawnedPos.y - playerPos.y;
        float width = spawnedPos.x - playerPos.x;
        angle = Mathf.Atan(height / width);
    }

    // the ball should move away from the character
    void MoveBall()
    {
        // move ball's pos away in opposite direction of player's current pos ()
        gameObject.transform.position = new Vector3(ballSpeed * Mathf.Cos(angle), ballSpeed * Mathf.Sin(angle), 0);
    }
}
