using UnityEngine;
using System.Collections;

public class Level : MonoBehaviour {

	public int level;
	SpriteRenderer sp;

	private Vector3 aux;
	private float angle;
	private float oscilation=0.2f;
	private Vector2 vPosOriginal;

	private bool _animate=false;
	public bool animate{ 
		get{
			return _animate;
		} 
		set{ 
			_animate=value; 
			if (!_animate){ 
				transform.position=vPosOriginal; 
			}  
		} 
	}

	// Use this for initialization
	void Start () {
		sp=GetComponent<SpriteRenderer>();
		Sprite text= Resources.Load<Sprite>("miniatures/zona"+level);
		if (text!=null)
			sp.sprite=text;

		vPosOriginal=transform.position;
		aux.z=transform.position.z;

	}
	
	// Update is called once per frame
	void Update () {
		if (animate){
			angle+=0.05f*Time.timeScale;
			aux.x=vPosOriginal.x+Mathf.Sin(angle)*oscilation*Time.timeScale;
			aux.y=vPosOriginal.y+Mathf.Sin(angle*2.2f)*oscilation*Time.timeScale;
			transform.position=aux;
		}
	}
}
