using UnityEngine;
using System.Collections;

public class Teletransportador : MonoBehaviour {

	public Teletransportador destiny;
	private bool activo=true;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float distance=Vector3.Distance(transform.position,Globals.pinky.transform.position);
		if (distance>0.5f)
			activo=true;

		if (distance<0.35f && activo && !Globals.pinky.isDied()){
			//Globals.pinky.transform.position=destiny.transform.position;
			destiny.Teletransporta(Globals.pinky.transform);
			Globals.pinky.becomeInmune();
		}

	}

	public void Teletransporta(Transform obj){
		obj.position=transform.position;
		activo=false;
	}
}
