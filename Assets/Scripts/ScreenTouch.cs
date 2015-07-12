using UnityEngine;
using System.Collections;

public class ScreenTouch : GuiElement {

	public ScreenTouch(){}

	public bool changeGravity(out Vector2 dir){ 
		dir=Vector2.zero;

		for (int i=0;i<cantTouch && i<10;++i){ //revisamos si alguna pulsación touch entre en el rango
			if (puls[i].z==0){//is used?
				dir=puls[i];
				dir.y=Screen.height-dir.y;
				puls[i].z=1;
				return true;
			}
		}

		if (cantTouch==0 && Input.GetMouseButton(0)){
			dir=Input.mousePosition;
			return true;
		}
		return false;
	}
}
