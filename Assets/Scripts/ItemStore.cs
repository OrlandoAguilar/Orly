using UnityEngine;
using System.Collections;

public class ItemStore {
	private int toolbarInt = 0;
	private string[]  toolbarstrings;
	private bool notstand=false;

	public Texture2D textDisk, textInmune, textFreeze,coinsTexture,textArrow,textJewel,textMas;
	PauseMenu father;

	public ItemStore(Texture2D textDisk,Texture2D textInmune,Texture2D textFreeze,Texture2D coinsTexture,Texture2D textArrow,Texture2D textJewel,Texture2D textMas,PauseMenu father){
		toolbarstrings=Globals.texts.itemToolbar;
		this.textDisk=textDisk; 
		this.textInmune=textInmune;
		this.textFreeze=textFreeze;
		this.coinsTexture=coinsTexture;
		this.father=father;
		this.textArrow=textArrow;
		this.textJewel=textJewel;
		this.textMas=textMas;
	}

	public void initializa(){
		notstand=false;
	}

	public bool Draw() {
		BeginPage(Globals.width-100,400);
		toolbarInt = GUILayout.Toolbar (toolbarInt, toolbarstrings);
		switch (toolbarInt) {
		case 0: Usar(); break;
		case 1: Comprar(); break;
		case 2: Monedas(); break;
		}
		EndPage();
		return notstand;
	}

	void Usar(){

		if (Globals.diskItem<1)
			GUI.enabled=false;
		if (GUI.Button(new Rect(0,85,80,50),textDisk)){
			father.UnPauseGame();
			notstand=true;
			Globals.pinky.Checkpoint(Globals.pinky.transform.position);
			Globals.diskItem--;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(0,135,80,33),string.Format("<color=green>{0}</color>",Globals.diskItem));
		GUI.Box(new Rect(84,85,617,80),Globals.texts.descriptionSave);


		if (Globals.heartItem<1)
			GUI.enabled=false;
		if (GUI.Button(new Rect(0,170,80,50),textInmune)){
			father.UnPauseGame();
			notstand=true;
			Globals.pinky.becomeInmune(12.0f);
			Globals.heartItem--;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(0,220,80,33),string.Format("<color=green>{0}</color>",Globals.heartItem));
		GUI.Box(new Rect(84,170,617,80),Globals.texts.descriptionInmune);

		if (Globals.iceItem<1)
			GUI.enabled=false;
		if (GUI.Button(new Rect(0,255,80,50),textFreeze)){
			father.UnPauseGame();
			notstand=true;
			Congelable.congelaCongelable(5.0f);
			Globals.iceItem--;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(0,305,80,33),string.Format("<color=green>{0}</color>",Globals.iceItem));
		GUI.Box(new Rect(84,255,617,80),Globals.texts.descriptionFreeze);

	}

	bool diskButtonSave=false;
	bool heartButtonSave=false;
	bool iceButtonSave=false;

	void Comprar(){
		if (Globals.coins<Globals.diskItemPrice)
			GUI.enabled=false;
		if (GUI.Button(new Rect(0,85,80,50),textDisk)){
			if (diskButtonSave){
				Globals.diskItem++;
				Globals.coins-=Globals.diskItemPrice;
			}
			diskButtonSave=false;
		}else{
			diskButtonSave=true;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(0,135,80,33),string.Format("<color=green>{0}</color>",Globals.diskItem));
		GUI.Box(new Rect(84,85,100,30),string.Format("<color=navy>{0}</color>",Globals.texts.price));
		GUI.Box(new Rect(84,115,100,30),string.Format("<color=navy>{0}</color>",Globals.diskItemPrice));

		if (Globals.coins<Globals.heartItemPrice)
			GUI.enabled=false;
		if (GUI.Button(new Rect(200,85,80,50),textInmune)){
			if (heartButtonSave){
				Globals.heartItem++;
				Globals.coins-=Globals.heartItemPrice;
			}
			heartButtonSave=false;
		}else{
			heartButtonSave=true;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(200,135,80,33),string.Format("<color=green>{0}</color>",Globals.heartItem));
		GUI.Box(new Rect(284,85,100,30),string.Format("<color=navy>{0}</color>",Globals.texts.price));
		GUI.Box(new Rect(284,115,100,30),string.Format("<color=navy>{0}</color>",Globals.heartItemPrice));

		if (Globals.coins<Globals.iceItemPrice)
			GUI.enabled=false;
		if (GUI.Button(new Rect(400,85,80,50),textFreeze)){
			if (iceButtonSave){
				Globals.iceItem++;
				Globals.coins-=Globals.iceItemPrice;
			}
			iceButtonSave=false;
		}else{
			iceButtonSave=true;
		}
		GUI.enabled=true;
		GUI.Box(new Rect(400,135,80,33),string.Format("<color=green>{0}</color>",Globals.iceItem));
		GUI.Box(new Rect(484,85,100,30),string.Format("<color=navy>{0}</color>",Globals.texts.price));
		GUI.Box(new Rect(484,115,100,30),string.Format("<color=navy>{0}</color>",Globals.iceItemPrice));

		DrawCoins();
	}

	private bool coinsToJewelsSave=false;
	private bool jewelsToCoinsSave=false;

	void Monedas(){
		if (Globals.jewels<1)
			GUI.enabled=false;
		if (GUI.Button(new Rect(300,100,100,80),textArrow)){
			if (jewelsToCoinsSave){
				Globals.coins+=Globals.coinPrice;
				Globals.jewels--;
			}
			jewelsToCoinsSave=false;
		}else{
			jewelsToCoinsSave=true;
		}

		GUI.Label(new Rect(100,120,100,50),"<color=green>1</color>");
		GUI.Label(new Rect(200,120,150,50),textJewel);
		GUI.Label(new Rect(450,120,100,50),"<color=green>10</color>");
		GUI.Label(new Rect(550,120,150,50),coinsTexture);

		GUI.enabled=true;

		if (Globals.coins<Globals.jewelPrice)
			GUI.enabled=false;

		if (GUI.Button(new Rect(300,200,100,80),textArrow)){
			if (jewelsToCoinsSave){
				Globals.coins-=Globals.jewelPrice;
				Globals.jewels++;
			}
			jewelsToCoinsSave=false;
		}else{
			jewelsToCoinsSave=true;
		}
		
		GUI.Label(new Rect(100,220,100,50),"<color=green>15</color>");
		GUI.Label(new Rect(200,220,150,50),coinsTexture);
		GUI.Label(new Rect(450,220,100,50),"<color=green>1</color>");
		GUI.Label(new Rect(550,220,150,50),textJewel);
		
		GUI.enabled=true;

		GUI.Label(new Rect(250,300,70,70),textJewel);
		GUI.Box(new Rect(330,315,80,33),string.Format("<color=green>{0}</color>",Globals.jewels));

		DrawCoins();


	}

	void DrawCoins(){
		GUI.Label(new Rect(500,300,70,70),coinsTexture);
		GUI.Box(new Rect(580,315,80,33),string.Format("<color=green>{0}</color>",Globals.coins));
		Color aux=GUI.backgroundColor;
		GUI.backgroundColor=Color.red;
		if (GUI.Button(new Rect(500,360,150,38),textMas)){
			TapJoy.ShowOffers();
		}
		GUI.backgroundColor=aux;
	}

	void ShowBackButton() {
		if (GUI.Button(new Rect(100, Globals.height - 100, 300, 90),Globals.texts.back)) {
			PlayerPrefs.SetInt(GlobalPrefs.totalJewels,Globals.jewels); 
			PlayerPrefs.SetInt(GlobalPrefs.totalDisk,Globals.diskItem); 
			PlayerPrefs.SetInt(GlobalPrefs.totalHeart,Globals.heartItem); 
			PlayerPrefs.SetInt(GlobalPrefs.totalIce,Globals.iceItem); 
			PlayerPrefs.SetInt(GlobalPrefs.totalCoins,Globals.coins); 
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
