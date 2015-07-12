using UnityEngine;
using System.Collections;

public class puerta : MonoBehaviour {

	private Vector2 vPosOriginal;
	private Vector3 aux;
	private float angle;
	public float oscilation=0.2f;
	// Use this for initialization
	void Start () {
		vPosOriginal=transform.position;
		aux.z=transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		angle+=0.05f*Time.timeScale;
		aux.x=vPosOriginal.x+Mathf.Sin(angle)*oscilation*Time.timeScale;
		aux.y=vPosOriginal.y+Mathf.Sin(angle*2.2f)*oscilation*Time.timeScale;
		transform.position=aux;
		if (Vector3.Distance(transform.position,Globals.pinky.transform.position)<0.35f && !Globals.pinky.isDied()){
			Globals.pinky.Gana();
		}
	}
}
