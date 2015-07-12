using UnityEngine;
using System.Collections;

public class Laser : Congelable {

	LineRenderer line;
	private Vector3 position;
	public float distance=10;
	public GameObject particles;
	private ParticleSystem part;
	// Use this for initialization
	void Start () {
		line=GetComponent<LineRenderer>();
		Vector3 pos=transform.position;
		pos.z=-0.3f;
		line.SetPosition(0,pos);
		GameObject partic=((GameObject)GameObject.Instantiate(particles,pos,transform.rotation));
		part=partic.GetComponent<ParticleSystem>();
		//part.transform.parent=transform;
	}
	
	// Update is called once per frame
	void Update () {
		float angle=transform.rotation.eulerAngles.z*Mathf.Deg2Rad;

		position.x=Mathf.Cos(angle)*distance+transform.position.x;
		position.y=Mathf.Sin(angle)*distance+transform.position.y;

		RaycastHit2D hit = Physics2D.Linecast(transform.position,position); 

		if (hit.point!=Vector2.zero){
			position=hit.point;
			part.enableEmission=true;
		}else{
			part.enableEmission=false;
		}

		position.z=-0.6f;
		part.transform.position=position;

		line.SetPosition(1,position);

		if (hit.transform==Globals.pinky.transform)
			Globals.pinky.Die();

		if (isCongelated()){
			updateCongelable();
		}

	}

	public void activate(bool state){
			gameObject.SetActive(state);
			if (part!=null)
				part.gameObject.SetActive(state);
	}

	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
		Animator anim=GetComponent<Animator>();
		if (anim!=null)
			anim.enabled=false;
	}
	
	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
		Animator anim=GetComponent<Animator>();
		if (anim!=null)
			anim.enabled=true;
		
	}
	
	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}
}
