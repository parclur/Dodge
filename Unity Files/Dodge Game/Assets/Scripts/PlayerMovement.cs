using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float playerSpeed = 10f;
    float jumpForce = 800f;
	float throwSpeed = 100f;

    bool ableToThrow = true;

    public bool onGround;
	public LayerMask ground;
	float groundTimer = 0f;

    Rigidbody2D rig;

    string playerHor;
    string playerVer;
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
            playerHor = "P1LSH";
            playerAimVer = "P1LSV";
            playerJump = "P1A";
            playerPickup = "P1X";
            playerAimHor = "P1RSH";
            playerAimVer = "P1RSV";
            playerThrow = "P1RT";
        }
        else if (gameObject.tag == "Player2")
        {
            playerHor = "P2LSH";
            playerAimVer = "P2LSV";
            playerJump = "P2A";
            playerPickup = "P2X";
            playerAimHor = "P2RSH";
            playerAimVer = "P2RSV";
            playerThrow = "P2RT";
        }
        else if (gameObject.tag == "Player3")
        {

            playerHor = "P3LSH";
            playerAimVer = "P3LSV";
            playerJump = "P3A";
            playerPickup = "P3X";
            playerAimHor = "P3RSH";
            playerAimVer = "P3RSV";
            playerThrow = "P3RT";
        }
        else if (gameObject.tag == "Player4")
        {

            playerHor = "P4LSH";
            playerAimVer = "P4LSV";
            playerJump = "P4A";
            playerPickup = "P4X";
            playerAimHor = "P4RSH";
            playerAimVer = "P4RSV";
            playerThrow = "P4RT";
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

			rig.AddForce (Vector2.up * 1.25f * jumpForce);
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
        Debug.Log(numBalls + " of balls");
       
		Debug.Log (Input.GetAxis (playerThrow));

		if (Input.GetAxis(playerThrow) != 0 && numBalls > 0 && ableToThrow)
		{
            Debug.Log("Thowing");
			float xMag = Input.GetAxis (playerAimHor);
			float yMag = Input.GetAxis (playerAimVer);
            float xMag2 = Input.GetAxis(playerHor);
            float yMag2 = Input.GetAxis(playerAimVer);


			GameObject ball = Instantiate (ballPrefab) as GameObject;

            float spawnX = gameObject.transform.position.x;
            float spawnY = gameObject.transform.position.y;

            // might have to create a new set for mag2 then mag1 for overriding
            // test to see how things work with this tho
            if (xMag2 > 0)
            {
                spawnX = gameObject.transform.position.x + 1.0f;
            }
            else if (xMag2 < 0)
            {
                spawnX = gameObject.transform.position.x - 1.0f;
            }

            if (yMag2 > 0)
            {
                spawnY = gameObject.transform.position.y + 1.0f;
            }
            else if (yMag2 < 0)
            {
                spawnY = gameObject.transform.position.y - 1.0f;
            }


            if (xMag > 0)
            {
                spawnX = gameObject.transform.position.x + 1.0f;
            }
            else if(xMag < 0)
            {
                spawnX = gameObject.transform.position.x - 1.0f;

            }

            if (yMag > 0)
            {
                spawnY = gameObject.transform.position.y + 1.0f;
            }
            else if (yMag < 0)
            {
                spawnY = gameObject.transform.position.y - 1.0f;

            }

            ball.transform.position = new Vector2(spawnX, spawnY);

            /*
            if (xMag < 0)
            {
                ball.transform.position = new Vector2(gameObject.transform.position.x - 1.0f, gameObject.transform.position.y);
            }
            else if (xMag > 0)
            {
                ball.transform.position = new Vector2(gameObject.transform.position.x + 1.0f, gameObject.transform.position.y);

            }
            else if (xMag == 0 && yMag > 0)
            {
                ball.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y + 1.0f);
            }
            else if (xMag == 0 && yMag < 0)
            {
                ball.transform.position = new Vector2(gameObject.transform.position.x , gameObject.transform.position.y - 1.0f);
            }
            */
            ball.GetComponent<Rigidbody2D> ().velocity = new Vector2 (throwSpeed * xMag * 0.5f, throwSpeed * yMag *0.5f);
			numBalls--;
            ableToThrow = false;
            StartCoroutine(AbleToShootAgain());
		}
	}

    IEnumerator AbleToShootAgain()
    {

        yield return new WaitForSeconds(0.2f);
        ableToThrow = true;
    }
}
