using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    float playerSpeed = 15f;
    float jumpPower = 3f;
    float forceSpeed = 10f;
    float forceJump = 150f;
    float forceFall = 25f;

    public bool onGround;

    Rigidbody2D rig;

	// Use this for initialization
	void Start () {
        rig = GetComponent<Rigidbody2D>();
        onGround = false;
	}
	
	// Update is called once per frame
	void Update () {
        // CheckMove();
        // CheckMoveInput();
        CheckMoveInputForce();
	}

    void CheckMove()
    {
        float xMove = Input.GetAxis("Horizontal");
        float yMove = Input.GetAxis("Jump") * jumpPower;

        Vector3 newMove = new Vector3(xMove, yMove, 0) * playerSpeed * Time.deltaTime;

        rig.MovePosition(transform.position + newMove);
    }

    public void Grounded()
    {
        onGround = true;
    }

    public void NotGrounded()
    {
        onGround = false;
    }

    void CheckMoveInput()
    {
        float xMove;
        float yMove;

        if(Input.GetAxis("Jump") > 0 && onGround)
        {
            yMove = Input.GetAxis("Jump") * jumpPower;
        }
        else
        {
            yMove = 0;
        }

        if(Input.GetAxis("Horizontal") != 0)
        {
            xMove = Input.GetAxis("Horizontal");
        }
        else
        {
            xMove = 0;
        }


        Vector3 newMove = new Vector3(xMove, yMove, 0) * playerSpeed * Time.deltaTime;

        rig.MovePosition(transform.position + newMove);
    }

    void CheckMoveInputForce()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            Debug.Log("Moving right");
            rig.AddForce(transform.right * forceSpeed);
            // rig.velocity = new Vector2(playerSpeed, rig.velocity.y);
        }
        else if (Input.GetAxis("Horizontal") < 0)
        {
            Debug.Log("Moving Left");
            rig.AddForce(transform.right * -forceSpeed);
            // rig.velocity = new Vector2(-playerSpeed, rig.velocity.y);
        }
        else
        {
            rig.velocity = new Vector2(rig.velocity.x * 0.8f, rig.velocity.y);
        }

        if (Input.GetAxis("Jump") != 0 && onGround)
        {
            Debug.Log("Jumping Low");
            rig.velocity = new Vector2(rig.velocity.x, 0.5f);
            rig.AddForce(transform.up * forceJump);
        }

        if (!onGround)
        {
            rig.AddForce(transform.up * (-forceJump/2));
            //rig.velocity = new Vector2(rig.velocity.x, -forceFall);
        }
    }
}
