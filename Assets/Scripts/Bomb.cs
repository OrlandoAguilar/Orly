using UnityEngine;
using System.Collections;

public class Bomb : MonoBehaviour {

	public Obstacle objetive;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player" && !Globals.pinky.isDied()){
			//Globals.pinky.becomeInmune();
			objetive.kill(1.3f);
			Destroy(gameObject);
		}
	}
}
