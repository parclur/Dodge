using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

    Vector2 spawn;

	public int possession = 0;

	Rigidbody2D rb;

	float speedThreshold = 1f;

	// Use this for initialization
	void Start () {
        SetSpawn();
        rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForDeadBall ();
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
        gameObject.transform.position = spawn;
    }

	void CheckForDeadBall()
	{
		float speed = Mathf.Sqrt(rb.velocity.x * rb.velocity.x + rb.velocity.y * rb.velocity.y);
		if (speed < speedThreshold)
		{
			possession = 0;
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
}
