using UnityEngine;
using System.Collections;

public class Joya : MonoBehaviour {

	public int score;
	public Object exploss;
	public AudioClip pickup;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(angleRotate);
		if (Vector3.Distance(transform.position,Globals.pinky.transform.position)<0.35f && !Globals.pinky.isDied()){
			//AudioSource aSource=gameObject.GetComponent<AudioSource>();
			//audio.Play();
			AudioSource.PlayClipAtPoint(pickup, transform.position);
			Globals.pinky.score+=score;
			Instantiate(exploss,new Vector3(transform.position.x,transform.position.y,-0.5997437f),Quaternion.identity);
			Destroy(gameObject);
		}
	}


}
