using UnityEngine;
using System.Collections;

public class Bee : Congelable {

	public Transform destiny;
	public float velocity=1.0f;
	private Vector3 start;

	// Use this for initialization
	void Start () {
		start=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
		float t=Mathf.PingPong(fTime*velocity,1.0f);
		transform.position=Vector3.Lerp(start,destiny.position,t);
		}
			updateCongelable();

	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Globals.pinky.Die();
		}
	}
	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
		GetComponent<Animator>().enabled=false;
	}
	
	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
		GetComponent<Animator>().enabled=true;
		
	}
	
	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}

}
