using UnityEngine;
using System.Collections;

public class SpikeBee : Congelable {

	
	public float velocity=0.03f;
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			transform.Translate(Vector3.right*velocity*Time.timeScale);
			if (Mathf.Abs(transform.position.x)>4.0f)
				Destroy(gameObject);
			if (Mathf.Abs(transform.position.y)>3.0f)
				Destroy(gameObject);
		}else{
			updateCongelable();
		}

	}
	
	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			Globals.pinky.Die();
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
