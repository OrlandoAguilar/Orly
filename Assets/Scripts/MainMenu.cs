using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	enum states { MAIN,OPCIONES,ADVICE}
	states actualState=states.MAIN;
	private bool newGame=true;
	float factorGravity=3.0f;
	private ScreenTouch screenTouch=new ScreenTouch();

	public GUISkin GuiSkin;
	Opciones op=new Opciones();
	// Use this for initialization
	void Start () {
		Time.timeScale=1;
		if (PlayerPrefs.HasKey(GlobalPrefs.totalJewels))
			newGame=false;
	}
	
	void Update () {
		if (Input.GetKeyDown("escape")) {
			Application.Quit();
		}

		Button.updateTouchs();

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

	void OnGUI ()
	{
		GuiTools.expand();
		GUI.skin = GuiSkin;
		switch(actualState){
		case states.MAIN:

			TitledAnimated();

			GUILayout.BeginArea( new Rect(50, 200, Globals.width-100, Globals.height-300));

			if (!newGame && GUILayout.Button(Globals.texts.continueText)){
				Application.LoadLevel("chooseLevel");
			}
			if (GUILayout.Button(Globals.texts.newGame)){
				if (newGame){
					beginNewGame();
				}else{
					actualState=states.ADVICE;
				}
			}
			if (GUILayout.Button(Globals.texts.options)){
				actualState=states.OPCIONES;
				op.initializa();
			}
			GUILayout.EndArea();
			break;
		case states.OPCIONES:
			if (op.Draw()){
				actualState=states.MAIN;
			}
			break;
		case states.ADVICE:
			GUILayout.BeginArea( new Rect(50, 50, Globals.width-100, Globals.height-100));
			GUILayout.Label(Globals.texts.adviceNewGame);
			if (GUILayout.Button(Globals.texts.Yes)){
				beginNewGame();
			}
			if (GUILayout.Button(Globals.texts.No)){
				actualState=states.MAIN;
			}
			GUILayout.EndArea();
			break;
		}
		
	}

	void beginNewGame(){
		PlayerPrefs.DeleteAll();
		Application.LoadLevel("beginAnimation");
	}

	void TitledAnimated(){
		float v=Time.time*5;
		string name=Globals.texts.nameGame;
		int x=Globals.width/2-80;
		int width;
		for (int z=0;z<name.Length;++z){
			string n=name[z].ToString();
			width=(int)(GuiSkin.GetStyle("Box").CalcSize(new GUIContent(n)).x);
			GUI.Box(new Rect(x,60+(int)(Mathf.Sin(v+z)*20),width,90),n);
			x+=width;
		}
	}

	void Awake() {
		FacebookClass.CallFBInit();
	}

}
