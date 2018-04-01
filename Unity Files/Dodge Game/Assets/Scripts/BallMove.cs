using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMove : MonoBehaviour {

    float ballSpeed;

    bool canMove = false;

    Vector3 playerPos;
    Vector3 spawnedPos;
    float angle;

	// Use this for initialization
	void Start () {
        spawnedPos = transform.position;
        ballSpeed = 0.05f;
	}
	
	// Update is called once per frame
	void Update () {
        if (canMove)
        {
            MoveBall();

        }
	}

    public void SetParentPos(Vector3 parentPos)
    {
        Debug.Log(parentPos);
        playerPos = parentPos;

        CalculateAngle();
    }

    public void SetAngle(float newAngle)
    {
        angle = newAngle;
        canMove = true;
    }

    void CalculateAngle()
    {
        // get the ball's pos
        // get the player's current pos

        // the ball's pos should be set

        // the player's pos should be set already

        // get the angle of player's pos to ball's pos
        
        // opp = ballY - playerY
        // adj = ballX - playerX        
        // hyp = distance = sqrt((ballX - playerX)^2 + (ballY - playerY)^2)
        // tan(theta) = opp/adj
        // theta = tan^-1(opp/adj)

        float height = spawnedPos.y;
        float width = spawnedPos.x;
        angle = Mathf.Atan(height / width);

        Debug.Log( "MY POS"  + height + " " + width);

        canMove = true;

    }

    // the ball should move away from the character
    void MoveBall()
    {
        // move ball's pos away in opposite direction of player's current pos ()
        float posX = gameObject.transform.position.x;
        float posY = gameObject.transform.position.y;


        gameObject.transform.position = new Vector3(posX + (ballSpeed * Mathf.Cos(angle)), posY + (ballSpeed * Mathf.Sin(angle)), 0);


    }
}
