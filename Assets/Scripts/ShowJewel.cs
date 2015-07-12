using UnityEngine;
using System.Collections;

public class ShowJewel : ActionDraw {

	public Texture2D jewel;
	public Texture2D arrow;

	public override void Draw(){
		float x=Mathf.Sin(Time.realtimeSinceStartup*2.5f)*30;
		GUI.Box (new Rect (Globals.width-200+x-80, Globals.height*2/3, 50, 50), arrow);
		GUI.Box (new Rect (Globals.width-200, Globals.height*2/3, 50, 50), jewel);
	}
}
