using UnityEngine;
using System.Collections;

public class GuiElement {

	protected static Vector3[] puls{
		get;
		private set;
	}

	protected static int cantTouch{
		get;
		private set;
	}

	public GuiElement(){
		puls= new Vector3[10];
	}

	public static void updateTouchs(){
		cantTouch=Input.touchCount;
		for (int i=0;i<cantTouch && i<10;i++){
			puls[i]=Input.GetTouch(i).position;
			puls[i].y=(Screen.height-puls[i].y); //due to the system coordinates
			puls[i].z=0;//not used
		}
	}


}
