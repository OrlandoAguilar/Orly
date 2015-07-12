using UnityEngine;
using System.Collections;

public class FireEmitter : Congelable {

	public Object spark;
	public float time=1.0f;
	public float phase=0.0f;
	public Color flameColor=Color.white;
	//siguiente disparo
	public float blast=2.0f;

	public bool dispara {
		get;
		private set;
	}

	float nextShoot=0.0f;
	float nextBlast=0.0f;
	float split=0.2f;

	// Use this for initialization
	void Start () {
		nextBlast=fTime+blast;
		dispara=true;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			if (dispara){
				if (fTime>nextShoot){
					GameObject oSpark=(GameObject) Instantiate(spark,transform.position, transform.rotation); 
					Spark comp = oSpark.GetComponent<Spark>();
					comp.father=this;
					comp.transform.parent=transform;
					oSpark.GetComponent<SpriteRenderer>().color=flameColor;
					Destroy(oSpark,time);
					nextShoot=fTime+split;
				}
			}

			if (fTime-phase>nextBlast){
				dispara=!dispara;
				nextBlast+=blast;
			}
		}
			updateCongelable();

	}

	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
	}

	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
		nextBlast+=fTime-nextBlast;
	}

	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}
}
