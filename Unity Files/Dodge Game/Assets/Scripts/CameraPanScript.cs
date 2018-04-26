using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanScript : MonoBehaviour {

	float minX; float maxX; float minY; float maxY;
	public GameObject[] Players;
	public Vector2 cameraBuffer;

	// Use this for initialization
	void Start () {
		Players = new GameObject[4];
		
	}
	
	// Update is called once per frame
	void Update () {
		UpdatePlayers ();
		CalculateFrame ();
		CalculatePosition ();


		
	}

	void UpdatePlayers(){
		Players [0] = GameObject.FindGameObjectWithTag ("Player1"); Players [1] = GameObject.FindGameObjectWithTag ("Player2"); 
		Players [2] = GameObject.FindGameObjectWithTag ("Player3"); Players [3] = GameObject.FindGameObjectWithTag ("Player4");
	}

	void CalculateFrame () {
		minX = Mathf.Infinity;
		maxX = -Mathf.Infinity;
		minY = Mathf.Infinity;
		maxY = -Mathf.Infinity;


		foreach (GameObject player in Players){
			Vector3 tempPlayer = player.transform.position;
			if (tempPlayer.x < minX) {
				minX = tempPlayer.x;
			}
			if (tempPlayer.x > maxX) {
				maxX = tempPlayer.x;
			}
			if (tempPlayer.y < minY) {
				minY = tempPlayer.y;
			}
			if (tempPlayer.y > maxY) {
				maxY = tempPlayer.y;
			}
		}
		
	}


	void CalculatePosition () {
	
		Vector3 center = Vector3.zero;
		Vector3 finalPos;

		foreach (GameObject player in Players) {
			center += player.transform.position;
		}
		finalPos = center / Players.Length;

		float sizeX = maxX - minX + cameraBuffer.x;
		float sizeY = maxY - minY + cameraBuffer.y;
		float windowSize = (sizeX > sizeY ? sizeX : sizeY);
		Debug.Log (windowSize);


		//Checking Var Limits
		if (windowSize < 10) {
			windowSize = 10;
		}
		if (finalPos.x < 0) {
			finalPos.x = 0;
		} 
		if (finalPos.y < 0) {
			finalPos.y = 0;
		}



		gameObject.GetComponent<Camera> ().orthographicSize = windowSize;
		gameObject.transform.position = new Vector3 (finalPos.x, finalPos.y, transform.position.z);
	

	}


}
