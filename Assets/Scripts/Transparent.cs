using UnityEngine;
using System.Collections;

public class Transparent : MonoBehaviour {

	SpriteRenderer spr;
	Color semitransp=new Color(1.0f,1.0f,1.0f,0.5f);
	// Use this for initialization
	void Start () {
		spr=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player")
			spr.color=semitransp;
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player")
			spr.color=Color.white;
	}
}
