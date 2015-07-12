using UnityEngine;
using System.Collections;

public class Phanthom : Congelable {

	private Vector3 originalPos;
	public float sizeJump=0.2f;
	public AudioClip monster;
	public Object exploss;

	// Use this for initialization
	void Start () {
		originalPos=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			float alpha=Mathf.PingPong(fTime/5.0f,1.0f);

		Vector3 aux=Vector3.zero;
		aux.x=originalPos.x+Mathf.Sin(alpha*6.3f)*sizeJump*Time.timeScale;
		aux.y=originalPos.y+Mathf.Cos(alpha*13.0f)*sizeJump*Time.timeScale;

		transform.position=aux;
		}
			updateCongelable();


	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			if (Globals.pinky.actualState==Pinky.state.ATACK){
				Globals.pinky.score+=4;
				AudioSource.PlayClipAtPoint(monster, transform.position);
				Instantiate(exploss,new Vector3(transform.position.x,transform.position.y,-0.5997437f),Quaternion.identity);
				Destroy(gameObject);
			}else{
				Globals.pinky.Die();
			}
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
