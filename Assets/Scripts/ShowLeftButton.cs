﻿using UnityEngine;
using System.Collections;

public class ShowLeftButton : ActionDraw {
	public Texture2D arrow;
	public Texture2D button;
	private float size=Mathf.Min(Screen.width/3,Screen.height/3)/2;
	public override void Draw(){
		if (Globals.supportTouchScreen){
			float x=Mathf.Sin(Time.realtimeSinceStartup*2.5f)*30;

			GUI.DrawTexture (new Rect (Globals.width-size, (Globals.height-size)/2, size, size), button);
			GUI.Box (new Rect (Globals.width+x-size-50, Globals.height/2-25, 50, 50), arrow);
		}
	}
}
