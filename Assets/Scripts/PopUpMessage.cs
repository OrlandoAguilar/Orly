using UnityEngine;
using System.Collections;

public class PopUpMessage : MonoBehaviour {
	private string text;
	private string nameWind;
	public GUISkin gs;
	private MonoBehaviour toSleep;

	public static PopUpMessage ShowPopUp(string text,string name,MonoBehaviour id=null){
		PopUpMessage pp=((GameObject)Instantiate(Resources.Load("GuiElements/PopUp"))).GetComponent<PopUpMessage>();
		pp.text=text;
		pp.nameWind=name;
		if (id!=null){
			pp.toSleep=id;
			id.enabled=false;
		}
		return pp;

	}

		private Rect windowRect = new Rect(20, 20, Globals.width-40, Globals.height-40);
	void OnGUI() 
	{
		GUI.skin=gs;
		windowRect = GUI.Window(0, windowRect, DoMyWindow, nameWind);
	}
	void DoMyWindow(int windowID) 
	{
		GUI.Label(new Rect(40,40,Globals.width-80,Globals.height-160),text);
		if (GUI.Button(new Rect(Globals.width/2-100, Globals.height-140, 200, 60), Globals.texts.close))
		{
			if (toSleep!=null)
			{
				toSleep.enabled=true;
			}
			Destroy(this.gameObject);
		}
			
	}
}
