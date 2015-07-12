using UnityEngine;
using System.Collections;

public class ShowRightButton : ActionDraw {
	public Texture2D arrow;
	public Texture2D button;
	private float size=Mathf.Min(Screen.width/3,Screen.height/3)/2;
	public override void Draw(){
		if (Globals.supportTouchScreen){
			float x=Mathf.Sin(Time.realtimeSinceStartup*2.5f)*30;
			
			GUI.DrawTexture (new Rect (0, (Globals.height-size)/2, size, size), button);

			Matrix4x4 matrixBackup = GUI.matrix;
			GUIUtility.RotateAroundPivot(180, new Vector2(x+size+50, Globals.height/2-25));
			GUI.Box (new Rect (x+size, Globals.height/2-100, 50, 50), arrow);
			GUI.matrix = matrixBackup;
		}
	}
}
