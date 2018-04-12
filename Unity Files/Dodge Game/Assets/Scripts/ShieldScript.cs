using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        //Physics.IgnoreLayerCollision(7, 8);
	}
	
	// Update is called once per frame
	void Update () {
        //GameObject.FindGameObjectWithTag("Floor").GetComponentInChildren<IgnoreShieldScript>().AddShield(gameObject);
	}

    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Floor")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        }
        /*if (col.gameObject.layer == 8)
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        }*/
    }
}
