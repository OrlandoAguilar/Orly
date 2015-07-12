using UnityEngine;
using System.Collections;

public static class GuiTools {
	static float native_width  = 800;
	static float native_height  = 600;
	
	public static void expand ()
	{
		//set up scaling
		float rx = Screen.width / native_width;
		float ry = Screen.height / native_height;
		GUI.matrix = Matrix4x4.TRS (Vector3.zero, Quaternion.identity, new Vector3 (rx, ry, 1)); 
		//now create your GUI normally, as if you were in your native resolution
		//The GUI.matrix will scale everything automatically.
	}
}	