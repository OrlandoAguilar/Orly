using UnityEngine;
using System.Collections;

public class Obstacle : MonoBehaviour {

	public Object exploss=null;
	private SpriteRenderer myRenderer;
	private bool destroy=false;
	ParticleSystem particle;
	// Use this for initialization
	void Start () {
		myRenderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		if (destroy){
			if (myRenderer.isVisible){//myRenderer.enabled==true &&
				particle=((GameObject)Instantiate(exploss,new Vector3(transform.position.x,transform.position.y,-0.5997437f),Quaternion.identity)).GetComponent<ParticleSystem>();
				myRenderer.enabled=false;
			}
			if (myRenderer.enabled==false && (particle==null || !particle.IsAlive())){
				ScrollCamera sc = Camera.main.GetComponent<ScrollCamera>();
				if (sc!=null)
					sc.RestoreLook();
					//Time.timeScale=1.0f;
				if (Globals.pinky!=null)
					Globals.pinky.Paused(false);
				Destroy(gameObject);
			}
		}
	}

	public void kill(float time){
		destroy=true;
		ScrollCamera sc = Camera.main.GetComponent<ScrollCamera>();
		sc.Look(gameObject.transform);
//		Destroy(gameObject,time);
		//myRenderer.color=Color.clear;
		//Time.timeScale=0.6f;
		Congelable.congelaCongelable(4.0f);
		Globals.pinky.Paused(true);
	}

/*	void OnApplicationQuit() {
		destroy=true; //if exits game, don't pause pinky.
	}*/

	/*void OnDestroy(){
		if (!destroy && GameObject.Find("Main Camera") != null){
			ScrollCamera sc = Camera.main.GetComponent<ScrollCamera>();
			if (sc!=null){
				sc.RestoreLook();
				//Time.timeScale=1.0f;
				if (Globals.pinky!=null)
					Globals.pinky.Paused(false);
			}
		}
	}*/
}
