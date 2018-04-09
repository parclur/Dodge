using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Physics.IgnoreLayerCollision(7, 8);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
