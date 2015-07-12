using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public static class FacebookClass {

	private static bool isInit = false;
	private static bool loged=false;
	public static string ApiQuery = "";

	private static void OnHideUnity(bool isGameShown)                                                   
	{                                                                                            
		FbDebug.Log("OnHideUnity");                                                              
		if (!isGameShown)                                                                        
		{                                                                                        
			// pause the game - we will need to hide                                             
			Time.timeScale = 0;                                                                  
		}                                                                                        
		else                                                                                     
		{                                                                                        
			// start the game back up - we're getting focus again                                
			Time.timeScale = 1;                                                                  
		}                                                                                        
	}    

	public static void CallFBInit()
	{
		if (!isInit)
			FB.Init(OnInitComplete, OnHideUnity);
	}
	
	private static void OnInitComplete()
	{
		isInit = true;
	}
	
	public static void CallFBLogin()
	{
		if (!loged)
			FB.Login("email,publish_actions", LoginCallback);
		loged=true;
	}
	
	static void  LoginCallback(FBResult result)
	{
		if (result.Error != null){
			Debug.Log( "Error Response:\n" + result.Error);
			loged=false;
		}else if(FB.IsLoggedIn) {
			Debug.Log(FB.UserId);
			loged=true;
		} else {
			Debug.Log("User cancelled login");
			loged=false;
		}
	}
	
	private static void CallFBLogout()
	{
		FB.Logout();
	}

	public static IEnumerator publicScreenshoot(MonoBehaviour toPause,string name){
		yield return new WaitForEndOfFrame();
		try{
			if (!loged){
				CallFBLogin();
			}
			TakeScreenshot(name);
		}catch{
			PopUpMessage.ShowPopUp(Globals.texts.withoutConnection,Globals.texts.error,toPause);
		}
		//
	}
	private static  void TakeScreenshot(string name) 
	{
		//yield return new WaitForEndOfFrame();
		if (FB.IsLoggedIn){

			
			int width = Screen.width;
			int height = Screen.height;
			var tex = new Texture2D(width, height, TextureFormat.RGB24, false);
			// Read screen contents into the texture
			tex.ReadPixels(new Rect(0, 0, width, height), 0, 0);
			tex.Apply();
			byte[] screenshot = tex.EncodeToPNG();

			var wwwForm = new WWWForm();
			wwwForm.AddBinaryData("image", screenshot, name+".png");
			
			FB.API("me/photos", Facebook.HttpMethod.POST, Callback, wwwForm);
		}
	}

	static void Callback(FBResult result)
	{
		if (result.Error != null)
			Debug.Log( "Error Response:\n" + result.Error);
		else if (!ApiQuery.Contains("/picture"))
			Debug.Log( "Success Response:\n" + result.Text);
		else
		{
			//lastResponseTexture = result.Texture;
		}
	}

	public static IEnumerator publishScore(int level, int score,MonoBehaviour toPause){
		yield return new WaitForEndOfFrame();
		try{
			if (!loged){
				CallFBLogin();
			}

			if (FB.IsLoggedIn)
			{

				var query = new Dictionary<string, string>();
				query["score"] = score.ToString();
				FB.API("/me/scores", Facebook.HttpMethod.POST, delegate(FBResult r) { FbDebug.Log("Result: " + r.Text); }, query);
			} 
		}catch{
			PopUpMessage.ShowPopUp(Globals.texts.withoutConnection,Globals.texts.error,toPause);
		}

		/*if (FB.IsLoggedIn)
		{
			var querySmash = new Dictionary<string, string>();

			querySmash["ScoreByLevel"] = "http://samples.ogp.me/284034268417686";
			print(FB.AppId);

			FB.API ("/me/" + "orl.ag.orly" + ":smash", Facebook.HttpMethod.POST, 
			        delegate(FBResult r) { FbDebug.Log("Result: " + r.Text); }, querySmash);
		}*/
		

	}
}
