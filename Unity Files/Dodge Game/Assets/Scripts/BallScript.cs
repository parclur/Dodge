using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {

	public int possession = 0;

	Rigidbody2D rb;

	float velocityThreshold = 10f;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		CheckForDeadBall ();
	}

	void CheckForDeadBall()
	{
		if (rb.angularVelocity < velocityThreshold)
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
