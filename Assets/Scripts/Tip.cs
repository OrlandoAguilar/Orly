using UnityEngine;
using System.Collections;

//public delegate void animationDraw();

public class Tip : MonoBehaviour {

	public string tip;
	private Vector3 orig;
	private bool showTip=false;
	public GUISkin tipSkin;
	private ActionDraw animationD;

	// Use this for initialization
	void Start () {
		orig=transform.position;
		animationD=GetComponent<ActionDraw>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 aux;
		aux.y=orig.y+Mathf.Sin(Time.time/15);
		transform.position=orig;
	}

	void OnTriggerEnter2D(Collider2D col){
		if (col.gameObject.tag == "Player" && !Globals.pinky.isDied()){
			if (showTip==false){
				Globals.pinky.GetComponent<PauseMenu>().enabled=false;
			}
			showTip=true;
			Time.timeScale=0;
		}
	}

	void OnGUI() {
		GuiTools.expand();
		if (showTip){
			GUI.skin = tipSkin;
			if (GUI.Button(new Rect (100,100,Globals.width-200,Globals.height-200), Globals.texts.getText(tip)) || Input.GetButton("Accept")) {
				Time.timeScale=1;
				Globals.pinky.GetComponent<PauseMenu>().enabled=true;
				Destroy(gameObject);
			}
			if (animationD!=null)
					animationD.Draw();
			}
	}
}
