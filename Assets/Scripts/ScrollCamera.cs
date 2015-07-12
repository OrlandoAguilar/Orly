using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Camera))]
public class ScrollCamera : MonoBehaviour {

	public float width=800;
	public float height=600;
	public Transform target;

	public float b_width=800;
	public float b_height=600;
	public Transform background;
	public float maxVelocity=0.2f;

	public Vector3 backgroundPhase;
	private float sWidth;
	private float sHeight;
	private Vector2 minCamera,maxCamera;

	private Transform originalTarget=null;

	// Use this for initialization
	void Start () {
		float fx=0;
		if (b_width>Screen.width && b_width<width && width!=Screen.width)
			fx=(b_width-Screen.width)/(width-Screen.width);

		float fy=0;

		if (b_height>Screen.height && b_height<height && height!=Screen.height)
			fy=(b_height-Screen.height)/(height-Screen.height);

		backgroundPhase=new Vector3(fx,fy,0);

		//para llevar los pixeles a unidades
		width/=100.0f;
		height/=100.0f;
		Vector3 off=camera.ScreenToWorldPoint(Vector3.zero);
		minCamera.x=-width/2-off.x;
		minCamera.y=-height/2-off.y;
		
		maxCamera.x=width/2+off.x;
		maxCamera.y=height/2+off.y;
		sWidth=off.x*-2;
		sHeight=off.y*-2;

		GetComponent<AudioListener>().enabled=false;

		if (Screen.width>b_width){
			background.localScale=new Vector3(Screen.width/b_width,1.0f,1.0f);
		}

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 vCameraPosition=transform.position;
		
		if (sWidth<width){
			float advance=target.position.x-vCameraPosition.x;
			if (advance>maxVelocity){
				advance=maxVelocity;
			}
			if (advance<-maxVelocity){
				advance=-maxVelocity;
			}
			vCameraPosition.x+=advance;
			if (vCameraPosition.x<minCamera.x)
				vCameraPosition.x=minCamera.x;
			
			if (vCameraPosition.x>maxCamera.x)
				vCameraPosition.x=maxCamera.x;

		}

		if (sHeight<height){
			float advance=target.position.y-vCameraPosition.y;
			if (advance>0.2f){
				advance=0.2f;
			}
			if (advance<-0.2f){
				advance=-0.2f;
			}
			vCameraPosition.y+=advance;
			if (vCameraPosition.y<minCamera.y)
				vCameraPosition.y=minCamera.y;
			
			if (vCameraPosition.y>maxCamera.y)
				vCameraPosition.y=maxCamera.y;

		}

		transform.position=vCameraPosition;

		background.position=ScrollBackground(ref vCameraPosition, ref backgroundPhase);

	}

	Vector3 ScrollBackground(ref Vector3 a, ref Vector3 b){
		return new Vector3(a.x-a.x*b.x,a.y-a.y*b.y,0);
	}

	public void Look(Transform obj){
		originalTarget=target;
		target=obj;
	}

	public void RestoreLook(){
		target=originalTarget;
		originalTarget=null;
	}
}
