using UnityEngine;
using System.Collections;

public class Bat : Congelable {

	Vector3 destiny;
	float speed=3.3f;
	public AudioClip monster;
	public Object exploss;
	public GameObject shoot;

	public static int dispara;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			float step = speed * Time.deltaTime;


			if ((Globals.pinky.transform.position-transform.position).sqrMagnitude<0.5f && 
			    Globals.pinky.actualState==Pinky.state.ATACK){
				destiny=new Vector3(Random.Range(-380,380)/100.0f,Random.Range(-380,380)/100.0f,0);
			}

			transform.position = Vector3.MoveTowards(transform.position, destiny, step);

			if (dispara>100){
				dispara=0;
				Vector3 dir=Globals.pinky.transform.position-transform.position;
				float angle=Mathf.Atan2(dir.y,dir.x)*Mathf.Rad2Deg;

				Instantiate(shoot,transform.position,Quaternion.Euler(0,0,angle));
			}
		}else{
			updateCongelable();
		}
	}

	public void Move(Vector3 pos){
		destiny=pos;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player"){
			if (Globals.pinky.actualState==Pinky.state.ATACK){
				AudioSource.PlayClipAtPoint(monster, transform.position);
				Instantiate(exploss,new Vector3(transform.position.x,transform.position.y,-0.5997437f),Quaternion.identity);
				Destroy(gameObject);
				EnemyFlyer.bats.Remove(this);
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
