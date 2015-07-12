using UnityEngine;
using System.Collections;

public class Spark : Congelable {

	public float velocity=0.004f;

	public FireEmitter father=null;

	//private Vector3 advance;

	// Use this for initialization
	void Start (){
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position=transform.position+advance*Time.timeScale;
			transform.Translate(Vector3.right*velocity*Time.timeScale);
			if (!father.dispara)
				Destroy(gameObject);
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player")
			Globals.pinky.Die();
	}
	protected override void OnCongela(){
		Destroy(gameObject);
	}
	
	protected override void OnDescongela(){
	}
	
	protected override void OnAlmost(){
	}
}
