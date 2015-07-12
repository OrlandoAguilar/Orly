using UnityEngine;
using System.Collections;

public class Disk : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player" && !Globals.pinky.isDied()){
			Globals.pinky.Checkpoint(transform.position);
			Destroy(gameObject);
		}
	}
}
