using UnityEngine;
using System.Collections;

public class EndGame : MonoBehaviour {

	public GUISkin creditsSkin;
	private string[] credits;
	private float time;
	Vector2 scrollPosition=Vector2.zero;
	int width;
	int height;
	// Use this for initialization
	void Start () {
		//Globals.pinky.Gana();
		gameObject.GetComponent<Animator>().enabled=false;
		Globals.pinky.GetComponent<Pinky>().Paused(true);
		time=Time.time;
		TextAsset text = Resources.Load("credits/Credits_"+Globals.languaje) as TextAsset;
		credits = text.text.Split('\n');

		width=Globals.width-150;
		height=credits.Length*(creditsSkin.label.fontSize+12);
	}
	
	// Update is called once per frame
	void Update () {
		scrollPosition.y=(Time.time-time)*50;
		if (scrollPosition.y>height){
			Globals.pinky.Gana();
			enabled=false;
		}
	}

	void OnGUI () {
		GuiTools.expand();
		GUI.skin = creditsSkin;
		GUI.Box(new Rect(0,0,Globals.width,Globals.height),"");
		scrollPosition = GUI.BeginScrollView(new Rect(50, 100, Globals.width-100, Globals.height-200), scrollPosition, new Rect(0, 0, width, height));
		
		GUILayout.BeginArea( new Rect(0,0,width,height));
		
		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}
		
		GUILayout.EndArea();
		GUI.EndScrollView();
		
		if (GUI.Button(new Rect(100, Globals.height - 100, 300, 50),Globals.texts.back)) {
			Globals.pinky.Gana();
			enabled=false;
		}
	}
}
