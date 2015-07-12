using UnityEngine;
using System.Collections;

public class Ant : Congelable {
	 
	public float velocity=2.3f;
	public float radius=1.0f;
	private Vector3 start;
	//public AudioClip sound;
	//SpriteRenderer spriteRenderer;
	// Use this for initialization
	void Start () {
		start=transform.position;
		//spriteRenderer=GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
		Vector3 pos=transform.position;
		float time=fTime*velocity;//*Time.timeScale;
		pos.x=start.x+Mathf.Cos(time)*radius;
		pos.y=start.y+Mathf.Sin(time)*radius;
		transform.position=pos;
		}
			updateCongelable();

	}

	void OnTriggerEnter2D(Collider2D col){
		//print(col.collider2D.constantForce);
		//col.

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
