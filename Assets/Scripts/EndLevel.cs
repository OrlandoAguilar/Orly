using UnityEngine;
using System.Collections;

public class EndLevel : MonoBehaviour {

	private int dieds;
	private int diedsAcum;

	public GUISkin GuiSkin;
	public AudioClip winSound;

	private int score;
	private int scoreAcum;

	private int total;
	private int totalAcum;

	private string time;
	private float timef;

	private int adv=0;

	private float BestTimeDone;
	private int BestTotalDone;

	// Use this for initialization
	void Start () {
		dieds= Globals.pinky.dieds;
		score=Globals.pinky.score;
		total=Mathf.FloorToInt(Globals.pinky.score*Globals.pinky.factor-Globals.pinky.dieds*Globals.factorReductorDieds);
		timef=Globals.pinky.time;
		time=Format.FormatTime(timef);

		string bestTime=GlobalPrefs.getLevelBestTime(Format.LL2LN(Application.loadedLevel));
		string bestTotal=GlobalPrefs.getLevelBestTotal(Format.LL2LN(Application.loadedLevel));
		if (PlayerPrefs.HasKey(bestTime)){
			BestTimeDone=PlayerPrefs.GetFloat(bestTime);
		}else{
			BestTimeDone=Mathf.Infinity;
		}
		
		if (PlayerPrefs.HasKey(bestTotal)){
			BestTotalDone=PlayerPrefs.GetInt(bestTotal);
		}else{
			BestTotalDone=int.MinValue;
		}

		AudioSource.PlayClipAtPoint(winSound, transform.position);

		Time.timeScale=0;

	}
	
	// Update is called once per frame
	void Update () {

		++adv;
		if (adv>2){
		if (diedsAcum<dieds){
			diedsAcum++;
		}else{
			if (scoreAcum<score){
				scoreAcum++;
			}else{
				if (totalAcum<total && total>0){
					totalAcum++;
				}
					if (totalAcum>total && total<0){
						totalAcum--;
					}
			}
		}
			adv=0;
		}
	}

	void OnGUI () {
		GuiTools.expand();
		GUI.skin = GuiSkin;

		GUI.Box(new Rect(0,0,Globals.width,Globals.height),"");

		//BeginPage(Screen.width/2,Screen.height/2);
		GUILayout.BeginArea( new Rect(30,20,Globals.width-60,Globals.height-180));

		var centeredStyle = GUI.skin.GetStyle("Label");
		centeredStyle.alignment = TextAnchor.UpperCenter;

		GUILayout.Label(string.Format("<size=70>{0} <color=red>{1}</color></size>",
		                              Globals.texts.level,
		                              Format.LL2LN(Application.loadedLevel)+1).Replace("olive","red"),
		                centeredStyle);
		centeredStyle.alignment = TextAnchor.MiddleLeft;
		GUILayout.Label(Globals.texts.deads+" = "+diedsAcum);
		GUILayout.Label(Globals.texts.jewelText+" = "+scoreAcum);
		GUILayout.Label(Globals.texts.factor+" = "+Globals.pinky.factor);

		string totalStringAux=string.Format("{0} = {2}x{3}-{4}x{5} = {1}", 
		                                    Globals.texts.total,
		                                    totalAcum,
		                                    Globals.pinky.factor,
		                                    Globals.pinky.score,
		                                    Globals.factorReductorDieds,
		                                    Globals.pinky.dieds);
		if (totalAcum>BestTotalDone)
			totalStringAux+=" "+Globals.texts.bestScore;
		GUILayout.Label(totalStringAux);

		string timeStringAux=Globals.texts.time+" = " + time;
		if (timef<BestTimeDone)
			timeStringAux+=" "+Globals.texts.bestScore;

		GUILayout.Label(timeStringAux);
		GUILayout.EndArea();


		if (totalAcum==total){
			if (GUI.Button(new Rect(Globals.width/2-100, Globals.height - 150, 200, 90),Globals.texts.Facebook)) {
				//StartCoroutine(FacebookClass.publishScore(Format.LL2LN(Application.loadedLevel),total,this));
				StartCoroutine(FacebookClass.publicScreenshoot(this,string.Format("{0} {1}={2}",Globals.texts.nameGame,Globals.texts.level, Format.LL2LN(Application.loadedLevel)+1)));
			}

		//if salir, save total and time, if best.
			if (GUI.Button(new Rect(Globals.width-250, Globals.height - 150, 200, 90),Globals.texts.continueText)) {
				saveData();
				Time.timeScale=1;
				Application.LoadLevel("chooseLevel");
			}

			if (GUI.Button(new Rect(50, Globals.height - 150, 200, 90),Globals.texts.restart)) {
				Time.timeScale=1;
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	void saveData(){
		if (BestTimeDone>timef){
			PlayerPrefs.SetFloat(GlobalPrefs.getLevelBestTime(Format.LL2LN(Application.loadedLevel)),timef);
		}

		if (BestTotalDone<total){
			PlayerPrefs.SetInt(GlobalPrefs.getLevelBestTotal(Format.LL2LN(Application.loadedLevel)),total);
		}
		
		if (total>0){
			Globals.jewels+=total;
			PlayerPrefs.SetInt(GlobalPrefs.totalJewels,Globals.jewels);
		}

		int level=Format.LL2LN(Application.loadedLevel)+1;
		if (level>=Format.countLevels()){
			level=Format.countLevels()-1;
		}
		PlayerPrefs.SetInt(GlobalPrefs.lastLevelPlayed,level);
	}

}
