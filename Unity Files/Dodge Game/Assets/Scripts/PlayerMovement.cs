using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float playerSpeed = 10f;
    float jumpForce = 800f;
	float throwSpeed = 100f;

    public bool onGround;
	public LayerMask ground;
	float groundTimer = 0f;

    Rigidbody2D rig;

    string playerHor;
    string playerJump;
	string playerPickup;
	string playerAimHor;
	string playerAimVer;
	string playerThrow;

	public CircleCollider2D pickupRad;

	int numBalls = 0;
	int maxBalls = 3;

	public GameObject ballPrefab;


	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        onGround = false;
        InitPlayer();
	}
	
	// Update is called once per frame
	void Update () {
		CheckGrounded ();
		CheckPickup ();
        CheckMove ();
		CheckThrow ();
	}

    void InitPlayer() //TODO add multiplayer options
    {
        if (gameObject.tag == "Player1")
        {
            playerHor = "Horizontal";
            playerJump = "Jump";
			playerPickup = "Pickup";
			playerAimHor = "RightJoystickHorizontal";
			playerAimVer = "RightJoystickVertical";
			playerThrow = "RightTrigger";
        }
        else if (gameObject.tag == "Player2")
        {

        }
        else if (gameObject.tag == "Player3")
        {

        }
        else if (gameObject.tag == "Player4")
        {

        }
    }



    void CheckMove()
    {
        float xMove = Input.GetAxis(playerHor);
        float yMove = Input.GetAxis(playerJump);

		rig.velocity = new Vector2(xMove * playerSpeed, rig.velocity.y);

		if (yMove != 0 && onGround)
		{
			onGround = false;
			groundTimer = 0.1f;

			rig.AddForce (Vector2.up * jumpForce);
		}
    }


	void CheckGrounded()
	{
		if (groundTimer == 0f)
		{
			RaycastHit2D rcLeft, rcRight;

			rcLeft = Physics2D.Raycast (new Vector2 (transform.position.x - 0.5f, transform.position.y - 0.5f), Vector2.down, 0.1f, ground);
			rcRight = Physics2D.Raycast (new Vector2 (transform.position.x + 0.5f, transform.position.y - 0.5f), Vector2.down, 0.1f, ground);

			if (rcLeft.transform != null || rcRight.transform != null)
			{
				onGround = true;
			} else
			{
				onGround = false;
			}
		}
		else
		{
			groundTimer -= Time.deltaTime;
			if (groundTimer <= 0f)
			{
				groundTimer = 0f;
			}
		}
	}


	void CheckPickup()
	{
		if (Input.GetAxis (playerPickup) != 0)
		{
			Collider2D[] hits = Physics2D.OverlapCircleAll (pickupRad.bounds.center, pickupRad.radius, LayerMask.GetMask ("Ball"));

			for (int i = 0; i < hits.GetLength (0) && numBalls < maxBalls; i++)
			{
				numBalls++;
				Destroy (hits [i].gameObject);
			}
		}
	}

	void CheckThrow()
	{
		Debug.Log (Input.GetAxis (playerThrow));
		if (Input.GetAxis(playerThrow) != 0 && numBalls > 1)
		{
			float xMag = Input.GetAxis (playerAimHor);
			float yMag = Input.GetAxis (playerAimVer);


			GameObject ball = Instantiate (ballPrefab) as GameObject;
			ball.transform.position = gameObject.transform.position;
			ball.GetComponent<Rigidbody2D> ().velocity = new Vector2 (throwSpeed * xMag, throwSpeed * yMag);
			numBalls--;
		}
	}
}
