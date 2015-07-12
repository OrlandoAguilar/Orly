using UnityEngine;
using System.Collections;

public class Heart: IUI{

	private Texture2D texture;
	private Rect rec;

	public Heart(Rect rec,Texture2D texture){
		this.texture=texture;
		this.rec=rec;
	}

	public void Draw(){
		GUI.DrawTexture( rec,texture );
	}
}
