using UnityEngine;
using System.Collections;

public class Controls : MonoBehaviour {

	public Texture2D buttonUp,buttonDown;
	public float factorGravity=3.0f;

	public static bool Accel=false,Deaccel=false;
	private Button bSlow=null,bAccel=null;
	private int size;

	private ScreenTouch screenTouch=new ScreenTouch();

	// Use this for initialization
	void Start () {
		if (Globals.supportTouchScreen){
			size=Mathf.Min(Screen.width/3,Screen.height/3);
			bSlow= new Button(new Rect(0,Screen.height/2-size/3,size,size),buttonUp,buttonDown);
			bAccel= new Button(new Rect(Screen.width-size,Screen.height/2-size/3,size,size),buttonUp,buttonDown);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Accel=false;
		Deaccel=false;
		if (Globals.supportTouchScreen){
			Button.updateTouchs();
			if (bAccel.Update()){
				Accel=true;
			}

			if (bSlow.Update()){
				Deaccel=true;
			}

		}


		if (Input.GetButton("deaccel")){
			Deaccel=true;
		}

		if (Input.GetButton("accel")){
			Accel=true;
		}

		Vector2 changeDir;
		if (Globals.activatedAccelerometer){
			Vector3 vGravityDirection = Vector3.zero;
			vGravityDirection = Input.acceleration;
			vGravityDirection.Normalize();
			Physics2D.gravity=vGravityDirection*factorGravity;
		}else if (screenTouch.changeGravity(out changeDir)) {
			Vector3 vS2WCoordinates=Camera.main.ScreenToWorldPoint(new Vector3(changeDir.x,changeDir.y,0)); //-Camera.main.transform.position.z
			Vector2 vGravityDirection=new Vector2(vS2WCoordinates.x-transform.position.x,vS2WCoordinates.y-transform.position.y);
			vGravityDirection.Normalize();
			Physics2D.gravity=vGravityDirection*factorGravity;
			
		}

		if (Input.GetButton("up")){
			Physics2D.gravity=Vector2.up*factorGravity;
		}
		if (Input.GetButton("down")){
			Physics2D.gravity=Vector2.up*-factorGravity;
		}
		if (Input.GetButton("left")){
			Physics2D.gravity=Vector2.right*-factorGravity;
		}
		if (Input.GetButton("right")){
			Physics2D.gravity=Vector2.right*factorGravity;
		}
	}

	
	void OnGUI() {
		if (Globals.supportTouchScreen && Time.timeScale!=0 && Globals.drawControls){
			bAccel.Draw();
			bSlow.Draw();

		}

	}
}
