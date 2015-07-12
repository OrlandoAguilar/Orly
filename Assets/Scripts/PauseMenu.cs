using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour
{
	
	public GUISkin skin;
	public GUISkin creditsSkin;
	public Texture2D textDisk, textInmune, textFreeze,coinsTexture,textArrow,textJewel,textMas;

	private float startTime = 0.1f;
	

	private bool paused=false;
	//public GameObject start;
	Vector2 scrollPosition = Vector2.zero;
	private string[] credits;

	public enum Page {
		None,Main,ItemStoreEn,Options,Credits
	}
	
	private Page currentPage;
	private float savedTimeScale;
	


	Opciones op= new Opciones();
	ItemStore itemStore;
	
	void Awake() {
		GUILayout.ExpandHeight(true);
		Time.timeScale = 1;
		itemStore= new ItemStore(textDisk, textInmune, textFreeze,coinsTexture,textArrow,textJewel,textMas,this);
		//PauseGame();
	}
	

	static bool IsDashboard() {
		return Application.platform == RuntimePlatform.OSXDashboardPlayer;
	}
	
	void LateUpdate () {
		if (Input.GetKeyDown("escape")) 
		{
			switch (currentPage) 
			{
			case Page.None: 
				PauseGame(); 
				break;
				
			case Page.Main: 
				if (!IsBeginning()) 
					UnPauseGame(); 
				break;
				
			default: 
				currentPage = Page.Main;
				break;
			}
		}
	}
	

	void ShowCredits() {
		GUI.skin = creditsSkin;
		if (credits==null){
			TextAsset text = Resources.Load("credits/Credits_"+Globals.languaje) as TextAsset;
			credits = text.text.Split('\n');
		}

		int width=Globals.width-150;
		int height=credits.Length*(GUI.skin.label.fontSize+12);

		scrollPosition = GUI.BeginScrollView(new Rect(50, 100, Globals.width-100, Globals.height-200), scrollPosition, new Rect(0, 0, width, height));

		GUILayout.BeginArea( new Rect(0,0,width,height));

		foreach(string credit in credits) {
			GUILayout.Label(credit);
		}

		GUILayout.EndArea();
		GUI.EndScrollView();

		GUI.skin = skin;
		ShowBackButton();
	}
	
	void ShowBackButton() {
		if (GUI.Button(new Rect(100, Globals.height - 100, 300, 90),Globals.texts.back)) {
			currentPage = Page.Main;
		}
	}
	


	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Globals.width - width) / 2, (Globals.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		if (currentPage != Page.Main) {
			ShowBackButton();
		}
	}
	
	bool IsBeginning() {
		return (Time.time < startTime);
	}
	
	
	void MainPauseMenu() {
		BeginPage(Globals.width-200,Globals.height-200);
		if (GUILayout.Button (IsBeginning() ? Globals.texts.play : Globals.texts.continueText)) {
			UnPauseGame();
			
		}

		if (GUILayout.Button (Globals.texts.itemStore)) {
			currentPage = Page.ItemStoreEn;
			itemStore.initializa();
		}
		if (GUILayout.Button (Globals.texts.options)) {
			currentPage = Page.Options;
			op.initializa();
		}

		/*if (GUILayout.Button(Globals.texts.PubishScreenshootFacebook))
		{
			UnPauseGame();
			StartCoroutine(FacebookClass.publicScreenshoot(this),Globals.texts.nameGame+((int)(Time.time*100)).ToString());
		}*/

		if (GUILayout.Button (Globals.texts.credits)) {
			currentPage = Page.Credits;
		}
		if (!IsBeginning() && GUILayout.Button (Globals.texts.restart)) {
			UnPauseGame();
			Application.LoadLevel(Application.loadedLevel);
		}
		if (GUILayout.Button (Globals.texts.exit)) {
			UnPauseGame();
			Application.LoadLevel(0);
		}
		EndPage();
	}
	
	public void PauseGame() {
		paused=true;
		savedTimeScale = Time.timeScale;
		Time.timeScale = 0;
		AudioListener.pause = true;
		Globals.pinky.GetComponent<Pinky>().enabled=false;
		Globals.pinky.GetComponent<Controls>().enabled=false;
		currentPage = Page.Main;
		scrollPosition = Vector2.zero;
	}
	
	public void UnPauseGame() {
		Time.timeScale = savedTimeScale;
		AudioListener.pause = false;
		credits=null;
		currentPage = Page.None;
		paused=false;
		Globals.pinky.GetComponent<Controls>().enabled=true;
		Globals.pinky.GetComponent<Pinky>().enabled=true;
	}
	
	bool IsGamePaused() {
		//return (Time.timeScale == 0);
		return paused;
	}
	
	void OnApplicationPause(bool pause) {
		if (IsGamePaused()) {
			AudioListener.pause = true;
		}
	}

	void OnGUI () {
		GuiTools.expand();
		if (skin != null) {
			GUI.skin = skin;
		}
		if (IsGamePaused()) {
			GUI.Box(new Rect(0,0,Globals.width,Globals.height),"");
			//GUI.color = statColor;
			switch (currentPage) {
			case Page.Main: MainPauseMenu(); break;
			case Page.Options: if (op.Draw()) currentPage=Page.Main; break;
			case Page.ItemStoreEn: if (itemStore.Draw()) if (currentPage==Page.ItemStoreEn)currentPage=Page.Main; break;
			case Page.Credits: ShowCredits(); break;
			}
		}   
	}

}