using UnityEngine;
using System.Collections;

public class Spikes : Congelable {
	public float dist=0.0f;
	public float velocity=0.3f;
	public enum _direction{ X,Y};
	public enum _sign{ Positive,Negative};
	public _direction direction=_direction.Y;
	public _sign sign=_sign.Positive;
	private Vector3 originalPos;
	// Use this for initialization
	void Start () {
		originalPos=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			if (dist!=0){
				float avance=Mathf.PingPong(fTime*velocity,dist);
				if (direction==_direction.Y){
					Vector3 pos= originalPos;
					if (sign==_sign.Positive)
						pos.y+=avance;
					else
						pos.y-=avance;
					transform.position=pos;
				}else{
					Vector3 pos= originalPos;
					if (sign==_sign.Positive)
						pos.x+=avance;
					else
						pos.x-=avance;

					transform.position=pos;
				}
			}
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
