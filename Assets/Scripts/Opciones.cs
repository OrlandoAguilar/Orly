using UnityEngine;
using System.Collections;

public class Opciones {

	private int toolbarInt = 0;
	private string[]  toolbarstrings;
	private bool notstand=false;
	public Opciones(){
		toolbarstrings=Globals.texts.toolbarstrings;
	}

	public void initializa(){
		notstand=false;
	}

	public bool Draw() {
		BeginPage(Globals.width-100,300);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: VolumeControl(); break;
		case 1: Qualities(); QualityControl(); break;
		case 2: Control(); break;
		}
		EndPage();
		return notstand;
	}

	void Qualities() {
		int level=QualitySettings.GetQualityLevel();
		string[] names=QualitySettings.names;
		GUILayout.Label(names[level]);
	}
	
	void QualityControl() {
		GUILayout.BeginHorizontal();
		if (GUILayout.Button(Globals.texts.decrease)) {
			QualitySettings.DecreaseLevel();
		}
		if (GUILayout.Button(Globals.texts.increase)) {
			QualitySettings.IncreaseLevel();
		}
		GUILayout.EndHorizontal();
	}
	
	void VolumeControl() {
		GUILayout.Label(Globals.texts.volume);
		AudioListener.volume = GUILayout.HorizontalSlider(AudioListener.volume, 0, 1);
	}
	
	void Control(){
		GUI.enabled=Globals.supportsAccelerometer;
		Globals.activatedAccelerometer = GUILayout.Toggle(Globals.activatedAccelerometer,Globals.texts.accelerometer);

		GUI.enabled=Globals.supportTouchScreen;
		Globals.drawControls = GUILayout.Toggle(Globals.drawControls,Globals.texts.botonesTouch);

		GUI.enabled=true;
	}

	void ShowBackButton() {
		if (GUI.Button(new Rect(100, Globals.height - 100, 300, 90),Globals.texts.back)) {
			PlayerPrefs.SetFloat(GlobalPrefs.volumeMusic,AudioListener.volume); 
			PlayerPrefs.SetInt(GlobalPrefs.qualityLevel,QualitySettings.GetQualityLevel()); 
			PlayerPrefs.SetInt(GlobalPrefs.activatedAccelerometer,Globals.activatedAccelerometer?1:0); 
			PlayerPrefs.SetInt(GlobalPrefs.drawControls,Globals.drawControls?1:0); 

			notstand=true;
		}
	}


	void BeginPage(int width, int height) {
		GUILayout.BeginArea( new Rect((Globals.width - width) / 2, (Globals.height - height) / 2, width, height));
	}
	
	void EndPage() {
		GUILayout.EndArea();
		ShowBackButton();
	}
}
