using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreShieldScript : MonoBehaviour {

    List<GameObject> shields = new List<GameObject>();
    int shieldAmount = 0;

	// Use this for initialization
	void Start () {
        //FindShields();
    }

    // Update is called once per frame
    void Update () {
        IgnoreTheDamnShields();
	}

    public void AddShield(GameObject newShield)
    {
        shields.Add(newShield);
        shieldAmount++;
    }
    

    void IgnoreTheDamnShields()
    {
        for(int i = 0; i < shieldAmount; i++)
        {
            Physics2D.IgnoreCollision(shields[i].GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        }
    }

   /* private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Shield")
        {
            Physics2D.IgnoreCollision(col.gameObject.GetComponent<BoxCollider2D>(), gameObject.GetComponent<BoxCollider2D>(), true);
        }
        
    }*/
}
