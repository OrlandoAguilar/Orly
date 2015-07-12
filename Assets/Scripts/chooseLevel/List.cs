/*
DeleteAll	Removes all keys and values from the preferences. Use with caution.
	DeleteKey	Removes key and its corresponding value from the preferences.
		GetFloat	Returns the value corresponding to key in the preference file if it exists.
			GetInt	Returns the value corresponding to key in the preference file if it exists.
				GetString	Returns the value corresponding to key in the preference file if it exists.
					HasKey	Returns true if key exists in the preferences.
						Save	Writes all modified preferences to disk.
							SetFloat	Sets the value of the preference identified by key.
							SetInt	Sets the value of the preference identified by key.
							SetString	Sets the value of the preference identified by key.
*/
using UnityEngine;
using System.Collections;

public class List : MonoBehaviour {

	public GameObject level;
	public Texture2D jewel;
	public GUISkin GuiSkin;

	GameObject [] list;
	ScrollCamera cam=null;
	private Vector3 mousePos;
	private int index=0;
	private int cantLevels;
	private int levelFollowing=0;
	LevelInfo[] levelInfo;
	float deltaTime;
	private Vector3 ubicFollowing;

	public GameObject particlesActual;

	private Color locked=new Color(0.1f,0.1f,0.1f,0.9f);
	private Color ready=new Color(0.8f,0.8f,0.8f,1.0f);
	// Use this for initialization
	void Start () {
		Time.timeScale=1;
		//PlayerPrefs.DeleteAll();
		/*if (PlayerPrefs.HasKey(GlobalPrefs.totalJewels)){
			Globals.jewels=PlayerPrefs.GetInt(GlobalPrefs.totalJewels);
		}*/

		cantLevels=Format.countLevels();
		Vector3 position=new Vector3(0,0,0);

		levelInfo = new LevelInfo[cantLevels+1];
		list=new GameObject[cantLevels+1];

		for (int z=0;z<cantLevels;++z){
			position.x+=3;
			list[z]=(GameObject)GameObject.Instantiate(level,position,Quaternion.identity);
			Level l=list[z].GetComponent<Level>();
			l.level=z;
			levelInfo[z]=new LevelInfo();
			if (PlayerPrefs.HasKey(GlobalPrefs.getLevelBestTime(z))){
				levelInfo[z].bestTime=PlayerPrefs.GetFloat(GlobalPrefs.getLevelBestTime(z));
				levelInfo[z].bestTotal=PlayerPrefs.GetInt(GlobalPrefs.getLevelBestTotal(z));
				levelFollowing=z;
				levelInfo[z].played=true;
			}else{
				list[z].GetComponent<SpriteRenderer>().color=locked;
				levelInfo[z].played=false;
			}
		}
		if (++levelFollowing>=cantLevels)
			--levelFollowing;
		list[levelFollowing].GetComponent<SpriteRenderer>().color=ready;
		ubicFollowing=list[levelFollowing].transform.position;

		cam=Camera.main.GetComponent<ScrollCamera>();

		if (PlayerPrefs.HasKey(GlobalPrefs.lastLevelPlayed)){
			index=PlayerPrefs.GetInt(GlobalPrefs.lastLevelPlayed);
		}
		selectTarget(index);


		if (PlayerPrefs.HasKey(GlobalPrefs.volumeMusic)){
			AudioListener.volume=PlayerPrefs.GetFloat(GlobalPrefs.volumeMusic); 
		}
		if (PlayerPrefs.HasKey(GlobalPrefs.qualityLevel)){
			QualitySettings.SetQualityLevel(PlayerPrefs.GetInt(GlobalPrefs.qualityLevel));
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonUp(0)){
			deltaTime=Time.time-deltaTime;
			Vector3 auxMousePos=Input.mousePosition;
			float dx=(mousePos.x-auxMousePos.x);
			dx=dx/(deltaTime*400);
			int ind=(int)dx;
			if (dx==0){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
			if(hit.transform!=null){
					int auxIndex=hit.transform.gameObject.GetComponent<Level>().level;
					if (index==auxIndex){
						loadLevel();
					}
					index=auxIndex;
					selectTarget(index);
			}
			}else{
				index+=ind;
				index=Mathf.Clamp(index,0,cantLevels-1);
				selectTarget(index);
			}
		}
		if (Input.GetMouseButtonDown(0)){
			mousePos=Input.mousePosition;
			deltaTime=Time.time;
		}

		if ( Input.GetButton("Accept")){
			loadLevel();
		}

		Vector3 gira=new Vector3(Mathf.Cos(Time.time)*1.7f,Mathf.Sin(Time.time)*1.3f,0);

		particlesActual.transform.position=ubicFollowing+gira;

	}

	void loadLevel(){
		PlayerPrefs.SetInt(GlobalPrefs.totalJewels,Globals.jewels);
		PlayerPrefs.SetInt(GlobalPrefs.lastLevelPlayed,index);
		PlayerPrefs.Save();
		Application.LoadLevel(Format.LN2LL(index));
	}

	void selectTarget(int target){
		Level l;
		if (cam.target!=null){
			l=cam.target.GetComponent<Level>();
			if (l!=null)
				l.animate=false;
		}
		cam.target=list[target].transform;
		l=cam.target.GetComponent<Level>();
		l.animate=true;

	}

	void OnGUI() {
		GuiTools.expand();
		GUI.skin = GuiSkin;

		if (GUI.Button(new Rect(Globals.width-200,Globals.height-90,190,80),Globals.texts.play))
		    loadLevel();

		GUI.Box (new Rect (Globals.width - 110, 50, 50, 50), jewel);
		GUI.Box (new Rect (Globals.width - 75, 27, 90, 90), Globals.jewels.ToString());

		//if (index<unlockedLevels){
		GUI.Box (new Rect (20, Globals.height-190, 200, 50),Globals.texts.level+" "+ (index+1).ToString());
		if (levelInfo[index].played){
			GUI.Box (new Rect (20, Globals.height-150, 200, 50), Globals.texts.bestScore);
			GUI.Box (new Rect (20, Globals.height-120, 200, 50), levelInfo[index].bestTotal.ToString());
			GUI.Box (new Rect (20, Globals.height-90, 200, 50), Format.FormatTime(levelInfo[index].bestTime));
		}else{
			GUI.Box (new Rect (20, Globals.height-150, 200, 50), Globals.texts.noPlayed);
		}

		//}
	}


}
