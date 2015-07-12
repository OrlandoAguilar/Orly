using UnityEngine;
using System.Collections;

public class Pikos : Congelable {

	private SpriteRenderer thisRendere;
	public float velocity=5.0f;
	// Use this for initialization
	void Start () {
		thisRendere=gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
		transform.Rotate(transform.forward*velocity);
		}else{
			updateCongelable();
		}
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"  && thisRendere.color.a>0.9){
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
