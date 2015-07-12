using UnityEngine;
using System.Collections;

public class Button : GuiElement,IUI{

	private Vector2 centerIn;
	private Rect rec;
	private float radius;
	private Texture2D buttonDown,buttonUp;
	private bool pressed;

	public Button(Rect rec,Texture2D buttonUp,Texture2D buttonDown){
		this.buttonDown=buttonDown;
		this.buttonUp=buttonUp;
		this.rec=rec;
		radius=Mathf.Min(rec.width,rec.height)/2;
		centerIn.x=rec.x+rec.width/2;
		centerIn.y=rec.y+rec.height/2;
	}

	protected bool pulled( Vector2 b,float distMax){
		float distCuad=distMax*distMax;
		Vector3 a;
		for (int i=0;i<cantTouch && i<10;++i){ //revisamos si alguna pulsación touch entre en el rango
			if (puls[i].z==0){//is used?
				a=puls[i];
				float r1=a.x-b.x;
				float r2=a.y-b.y;
				float dist=r1*r1+r2*r2;
				if (dist<distCuad){
					puls[i].z=1;
					return true;
				}

			}
			
		}
		//si ninguna entra, enviamos la info del ratón.
		/*if (Input.GetMouseButton(0)){
			a=Input.mousePosition;
			float r1=a.x-b.x;
			float r2=Screen.height-a.y-b.y;
			float dist=r1*r1+r2*r2;
			if (dist<distCuad){
				return true;
			}
		}*/
		return false;
	}

	public bool Update(){
		pressed=pulled(centerIn,radius);
		/*if (pressed){
			pressed=true;
		}else{
			pressed=false;

		}*/
		return pressed;
	}

	public void Draw(){
		if (pressed){
			GUI.DrawTexture( rec,buttonDown );
		}else{
			GUI.DrawTexture( rec,buttonUp );
		}

	}

}
