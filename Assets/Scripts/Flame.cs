using UnityEngine;
using System.Collections;

public class Flame : Congelable {

	public float velocity=0.004f;
	
	//private Vector3 advance;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			transform.Translate(Vector3.right*velocity*Time.timeScale);
		}else{
			updateCongelable();
		}
	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Globals.pinky.Die();
		}else if (col.gameObject.tag!="Bullet" && col.gameObject.tag!="Enemy"){
			Destroy(gameObject);
		}
	}

	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
	}
	
	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
	}
	
	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}

}
