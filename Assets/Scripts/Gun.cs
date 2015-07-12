using UnityEngine;
using System.Collections;

public class Gun : Congelable {

	public float everyBlast=1.0f;
	public Object spark;
	public int shift=0;

	private float acumulator;
	private static Vector3 angleRotate=new Vector3(0.0f,0.0f,45.0f);
	// Use this for initialization
	void Start () {
		acumulator=fTime;
		transform.Rotate(angleRotate*shift);
	}
	
	// Update is called once per frame
	void Update () {
		if (!isCongelated()){
			if ( fTime - acumulator > everyBlast){
				transform.Rotate(angleRotate);
				acumulator+=everyBlast;

				GameObject flame=(GameObject)Instantiate(spark,transform.position, transform.rotation); 
				flame.transform.Translate(0.2f,0.0f,0.0f);

				flame=(GameObject)Instantiate(spark,transform.position, transform.rotation); 
				flame.transform.Rotate(angleRotate*4);
				flame.transform.Translate(0.2f,0.0f,0.0f);

			}
		}
			updateCongelable();

	}

	protected override void OnCongela(){
		GetComponent<SpriteRenderer>().color=new Color(0.5f,0.85f,0.89f,0.9f);
	}
	
	protected override void OnDescongela(){
		GetComponent<SpriteRenderer>().color=Color.white;
		acumulator+=fTime-acumulator;
	}
	
	protected override void OnAlmost(){
		GetComponent<SpriteRenderer>().color=Color.gray;
	}
}
