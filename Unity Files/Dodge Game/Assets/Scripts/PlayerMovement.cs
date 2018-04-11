using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    Vector2 spawn;

    float playerSpeed = 10f;
    float jumpForce = 800f;
	float throwSpeed = 100f;

    public bool ableToThrow = false;
    public bool ableToPickUp = true;
    public bool ableToShield = true;

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
    string playerShield;

	public CircleCollider2D pickupRad;

    string ballSavedName;
	int numBalls = 0;
	int maxBalls = 1;
    int shieldHealth = 1;
    public bool isOut = false;

	public GameObject ballPrefab;
    public GameObject shieldPrefab;
    public GameObject cursorPrefab;

	public int team;

	float iFrameTimer = 0f;
	Color color;

	// Use this for initialization
	void Start () {

        cursorPrefab = Instantiate(cursorPrefab);
        shieldPrefab = Instantiate(shieldPrefab);
        cursorPrefab.transform.parent = gameObject.transform;
        shieldPrefab.transform.parent = gameObject.transform;
        cursorPrefab.transform.position = gameObject.transform.position;
        shieldPrefab.transform.position = gameObject.transform.position;

        rig = GetComponent<Rigidbody2D>();
        onGround = false;
		color = GetComponent<SpriteRenderer>().color;
        InitPlayer();
	}
	
	// Update is called once per frame
	void Update () {

        if(!isOut)
        {
            gameObject.SetActive(true);
            SetCursor();
            CheckGrounded();
            CheckPickup();
            CheckMove();
            CheckThrow();
            CheckShield2();
            UpdateIframes();
        }
        else
        {
            gameObject.SetActive(false);
        }

	}


    public void NotOutAnymore()
    {
        isOut = false;
    }


    public void ResetPlayer()
    {
        gameObject.transform.position = spawn;
        shieldHealth = 1;

        if(numBalls>0)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.name = ballSavedName;
            numBalls = 0;
        }

        isOut = false;
        gameObject.SetActive(true);


    }


    void InitPlayer() //TODO add multiplayer options
    {
        spawn = gameObject.transform.position;
        if (gameObject.tag == "Player1")
        {
            playerHor = "P1LSH";
            playerVer = "P1LSV";
            playerJump = "P1A";
            playerPickup = "P1X";
            playerAimHor = "P1RSH";
            playerAimVer = "P1RSV";
            playerThrow = "P1RT";
            playerShield = "P1LT";
        }
        else if (gameObject.tag == "Player2")
        {
            playerHor = "P2LSH";
            playerVer = "P2LSV";
            playerJump = "P2A";
            playerPickup = "P2X";
            playerAimHor = "P2RSH";
            playerAimVer = "P2RSV";
            playerThrow = "P2RT";
            playerShield = "P2LT";

        }
        else if (gameObject.tag == "Player3")
        {

            playerHor = "P3LSH";
            playerVer = "P3LSV";
            playerJump = "P3A";
            playerPickup = "P3X";
            playerAimHor = "P3RSH";
            playerAimVer = "P3RSV";
            playerThrow = "P3RT";
            playerShield = "P3LT";

        }
        else if (gameObject.tag == "Player4")
        {

            playerHor = "P4LSH";
            playerVer = "P4LSV";
            playerJump = "P4A";
            playerPickup = "P4X";
            playerAimHor = "P4RSH";
            playerAimVer = "P4RSV";
            playerThrow = "P4RT";
            playerShield = "P4LT";

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

			rcLeft = Physics2D.Raycast (new Vector2 (transform.position.x - 0.5f, transform.position.y - 0.55f), Vector2.down, 0.05f, ground);
			rcRight = Physics2D.Raycast (new Vector2 (transform.position.x + 0.5f, transform.position.y - 0.55f), Vector2.down, 0.05f, ground);

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
		if (Input.GetAxis (playerPickup) != 0 && ableToPickUp)
		{
            ableToPickUp = false;
            ableToThrow = false;

			Collider2D[] hits = Physics2D.OverlapCircleAll (pickupRad.bounds.center, pickupRad.radius, LayerMask.GetMask ("Ball"));
			for (int i = 0; i < hits.GetLength (0) && numBalls < maxBalls; i++)
			{
                ballSavedName = hits[i].gameObject.name;
                numBalls++;
				Destroy (hits [i].gameObject);
			}
		}
	}


    void SetCursor()
    {
        float spawnX;
        float spawnY;

        spawnX = Input.GetAxis(playerHor);
        spawnY = Input.GetAxis(playerVer);

        spawnX = Input.GetAxis(playerAimHor);
        spawnY = Input.GetAxis(playerAimVer);

        cursorPrefab.transform.position = new Vector2(gameObject.transform.position.x + spawnX, gameObject.transform.position.y + spawnY);
    }


	void CheckThrow()
	{

        if (Input.GetAxis(playerThrow) != 0 && numBalls > 0 && ableToThrow)
		{
            Debug.Log("Thowing");

            float xMag = Input.GetAxis(playerAimHor);
            float yMag = Input.GetAxis(playerAimVer);
            float xMag2 = Input.GetAxis(playerHor);
            float yMag2 = Input.GetAxis(playerVer);

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
            ball.name = ballSavedName;
            ball.GetComponent<BallScript>().possession = team;
			ball.GetComponent<BallScript>().UpdateColor();

            if (xMag != 0 || yMag != 0)
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed * xMag * 0.5f, throwSpeed * yMag * 0.5f);
            else
                ball.GetComponent<Rigidbody2D>().velocity = new Vector2(throwSpeed * xMag2 * 0.5f, throwSpeed * yMag2 * 0.5f);

            numBalls--;
            ableToThrow = false;
		}

        if (numBalls == 0)
        {
            StartCoroutine(AbleToPickUpAgain());
        }
        else
            StartCoroutine(AbleToShootAgain());
	}


    void CheckShield2()
    {
        if(Input.GetAxis(playerShield) > 0)
        {

            Debug.Log("Shielding");
            float xMag = Input.GetAxis(playerAimHor);
            float yMag = Input.GetAxis(playerAimVer);
            float xMag2 = Input.GetAxis(playerHor);
            float yMag2 = Input.GetAxis(playerVer);

            if (shieldHealth > 0)
            {
                // activate the shield
                shieldPrefab.SetActive(true);

                // putting the shield in place
                float spawnX = gameObject.transform.localPosition.x;
                float spawnY = gameObject.transform.localPosition.y;
                float spawnDist = 0.9f;

                // positioning the shield and angling the shield
                if(Mathf.Abs(xMag) > 0 || Mathf.Abs(yMag) > 0)
                {
                    // setting pos
                    if (xMag > 0)
                    {
                        spawnX = gameObject.transform.localPosition.x + spawnDist;
                    }
                    else if (xMag < 0)
                    {
                        spawnX = gameObject.transform.localPosition.x - spawnDist;
                    }
                    if (yMag > 0)
                    {
                        spawnY = gameObject.transform.localPosition.y + spawnDist;
                    }
                    else if (yMag < 0)
                    {
                        spawnY = gameObject.transform.localPosition.y - spawnDist;
                    }
                    if (xMag == 0 && yMag == 0)
                    {
                        spawnX = gameObject.transform.localPosition.x + spawnDist;
                    }

                    // seting the angle
                    if (xMag > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                        if (yMag > 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        }
                        else if (yMag < 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                        }

                    }
                    else if (xMag < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                        if (yMag > 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                        }
                        else if (yMag < 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        }
                    }
                    else
                    {
                        if (yMag != 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                    }

                }
                else
                {
                    // seting the pos
                    if (xMag2 > 0)
                    {
                        spawnX = gameObject.transform.localPosition.x + spawnDist;
                    }
                    else if (xMag2 < 0)
                    {
                        spawnX = gameObject.transform.localPosition.x - spawnDist;
                    }
                    if (yMag2 > 0)
                    {
                        spawnY = gameObject.transform.localPosition.y + spawnDist;
                    }
                    else if (yMag2 < 0)
                    {
                        spawnY = gameObject.transform.localPosition.y - spawnDist;
                    }
                    if(xMag2 == 0 && yMag2 ==0)
                    {
                        spawnX = gameObject.transform.localPosition.x + spawnDist;
                    }

                    // seting the angle
                    if (xMag2 > 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                        if (yMag2 > 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        }
                        else if (yMag2 < 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                        }

                    }
                    else if (xMag2 < 0)
                    {
                        shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 0);
                        if (yMag2 > 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 135);
                        }
                        else if (yMag2 < 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 45);
                        }
                    }
                    else
                    {
                        if (yMag2 != 0)
                        {
                            shieldPrefab.transform.eulerAngles = new Vector3(0, 0, 90);
                        }
                    }

                }

                shieldPrefab.transform.position = new Vector2(spawnX, spawnY);

            }
            
        }
        else
        {
            shieldPrefab.SetActive(false);
            
        }

        if(shieldHealth < 1)
        {
            StartCoroutine(AbleToShieldAgain());
        }
    }


    IEnumerator AbleToShieldAgain()
    {
        yield return new WaitForSeconds(2.0f);
        ableToShield = true;
        shieldHealth = 1;
    }


    IEnumerator AbleToShootAgain()
    {
        yield return new WaitForSeconds(0.3f);
        ableToThrow = true;
    }


    IEnumerator AbleToPickUpAgain()
    {
        yield return new WaitForSeconds(0.3f);
        ableToPickUp = true;
    }


    void OnCollisionEnter2D(Collision2D col)
	{
        if(col.otherCollider.transform.tag == "Shield")
        {
            Debug.Log("You got blocked bitch");
            shieldHealth--;
        }
        else
        {
            if (col.transform.tag == "Ball" && col.transform.GetComponent<BallScript>().possession != 0 && col.transform.GetComponent<BallScript>().possession != team)
            {
                isOut = true;

                if(numBalls > 0)
                {
                    Instantiate(ballPrefab, gameObject.transform);
                    numBalls--;
                }

                iFrameTimer = 0.5f;
            }
        }

	}


	void UpdateIframes()
	{
		if (iFrameTimer <= 0f)
		{
			iFrameTimer = 0f;
			GetComponent<SpriteRenderer>().color = color;
		}
		else{
			iFrameTimer -= Time.deltaTime;
			if ((int)(iFrameTimer * 6) % 2 == 0)
			{
				GetComponent<SpriteRenderer>().color = Color.white;
			}
			else{
				GetComponent<SpriteRenderer>().color = color;
			}
		}

	}
}
