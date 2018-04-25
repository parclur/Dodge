using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Vector2 spawn;

	public int possession = 0;

	Rigidbody2D rb;

	float speedThreshold = 20f;

    GameObject thrower;
    string possessorName;

	// Use this for initialization
	void Start () {
        thrower = null;
        SetSpawn();
        rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForDeadBall ();
	}

    public void ChangeTeam()
    {
        if(possession == 1){
            possession = 2;
        }
        if(possession == 2){
            possession = 1;
        }
        else{
            possession = 0;
        }
        UpdateColor();
    }

    void SetSpawn()
    {
        if (name == "Ball1")
            spawn = GameObject.Find("Ball_Start_Point_0").transform.position;
        else if (name == "Ball2")
            spawn = GameObject.Find("Ball_Start_Point_1").transform.position;
        else if (name == "Ball3")
            spawn = GameObject.Find("Ball_Start_Point_2").transform.position;
        else if (name == "Ball4")
            spawn = GameObject.Find("Ball_Start_Point_3").transform.position;
    }

    public void ResetPos()
    {
        SetSpawn();
        thrower = null;
        gameObject.transform.position = spawn;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }

    void CheckForDeadBall()
	{

        if(rb.velocity.x > 25)
        {
            rb.velocity.Set(25, 0);
        }
        if(rb.velocity.x < -25)
        {
            rb.velocity.Set(-25, 0);
        }
		float speed = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);

        
		if (speed < speedThreshold)
		{
			possession = 0;
            //thrower = null;
			UpdateColor ();
		}
	}

	public void UpdateColor()
	{
		SpriteRenderer sr = GetComponent<SpriteRenderer> ();

		if (possession == 0)
		{
			sr.color = Color.white;
		}
		else if (possession == 1)
		{
			sr.color = Color.blue;
		}
		else if (possession == 2)
		{
			sr.color = Color.red;
		}
	}

    public void SetPossessorName(string newName)
    {
        possessorName = newName;
    }

    public void SetThrower(GameObject player)
    {
        thrower = player;
        possessorName = player.name;
        possession = thrower.GetComponent<PlayerMovement>().team;
    }

    public void SendKillInfo(GameObject deadObj)
    {
        GameObject GameMan = GameObject.Find("GameManager");

        if(GameMan.GetComponent<ManagerScript>())
        {
            Debug.Log(deadObj.name + " is dead");
            Debug.Log(possessorName + "got a kill");
            GameMan.GetComponent<ManagerScript>().IncrementPlayerDeaths(deadObj.name);
            GameMan.GetComponent<ManagerScript>().IncrementPlayerKills(possessorName);
        }
    }

}
