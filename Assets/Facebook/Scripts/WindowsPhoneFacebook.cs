using UnityEngine;
using System.Collections;
using System.Collections.Generic;
#if UNITY_WP8

using FacebookCS.ClientWP8;

namespace Facebook
{
	
	public class WindowsPhoneFacebook : AbstractFacebook, IFacebook{
		
		public const int BrowserDialogMode = 0;
			
		FacebookSessionClient sessionC;
		FacebookCS.FacebookClient fbClient;
		
		FacebookDelegate cb = null;
		
		public override int DialogMode { get { return BrowserDialogMode; } set { } }
		
		protected override void OnAwake()
        {
           	userId = "";
			accessToken = "";
        }
#region Init
        public override void Init(
            InitDelegate onInitComplete,
            string appId,
            bool cookie = false,
            bool logging = true,
            bool status = true,
            bool xfbml = false,
            string channelUrl = "",
            string authResponse = null,
            bool frictionlessRequests = false,
            Facebook.HideUnityDelegate hideUnityDelegate = null)
        {
            sessionC = new FacebookSessionClient(appId);
			
			StartCoroutine(LoadSession());
			
			if (onInitComplete != null){
                onInitComplete();
            }
			
        }

        
        #endregion

        public override void Login(string scope = "", FacebookDelegate callback = null)
        {
			
           	sessionC.LoginAsync(scope, LoginComplete);

			isLoggedIn = true;
			
			cb = callback;
			//AddAuthDelegate(callback);
		}
		
		private void LoginComplete(){
			
			userId = sessionC.CurrentSession.FacebookId;
			accessToken = sessionC.CurrentSession.AccessToken;
			
			fbClient = new FacebookCS.FacebookClient(accessToken);
			fbClient.AppId = sessionC.AppId; 

			isLoggedIn = true;
			
			SaveSession();
			
			if(cb != null) 
				cb(new FBResult("Login sucessful", null));
			 
		}

        public override void Logout()
        {
			if(isLoggedIn){
            	sessionC.Logout(null);
				isLoggedIn = false;
				userId = "";
				accessToken = "";
				fbClient = null;
				isLoggedIn = false;
				DeleteSession();
			}
		}

        public override void AppRequest(
            string message,
            string[] to = null,
            string filters = "",
            string[] excludeIds = null,
            int? maxRecipients = null,
            string data = "",
            string title = "",
            FacebookDelegate callback = null)
        {
			throw new UnityException("There is no Facebook AppRequest on Windows Phone 8");
        }

        public override void FeedRequest(
            string _toId = "",
            string _link = "",
            string _linkName = "",
            string _linkCaption = "",
            string _linkDescription = "",
            string _picture = "",
            string _mediaSource = "",
            string _actionName = "",
            string _actionLink = "",
            string _reference = "",
            Dictionary<string, string[]> _properties = null,
            FacebookDelegate callback = null)
        {
			if(!isLoggedIn) return;
			
			string url = "https://graph.facebook.com/" + _toId +"/feed?method=post";
			if(_link.Length > 0) url += "&link=" +_toId;
			if(_linkName.Length > 0) url += "&name=" +_linkName;
			if(_linkCaption.Length > 0) url += "&caption=" +_linkCaption;
			if(_linkDescription.Length > 0) url += "&description=" +_linkDescription;
			if(_picture.Length > 0) url += "&picture=" +_picture;
			if(_mediaSource.Length > 0) url += "&source=" +_mediaSource;
			
			if(_actionName.Length > 0 && _actionLink.Length > 0) { 
				url += string.Format("&actions=[{\"name\":\"{0}\",\"link\":\"{1}\"}]", _actionName, _actionLink);
			}

			if(_reference.Length > 0) url += "&type" +_reference;
			
			url += "&access_token=" + accessToken;
			
			StartCoroutine(GetFBResult(url, callback));
		}
        	

        public override void Pay(
            string product,
            string action = "purchaseitem",
            int quantity = 1,
            int? quantityMin = null,
            int? quantityMax = null,
            string requestId = null,
            string pricepointId = null,
            string testCurrency = null,
            FacebookDelegate callback = null)
        {
            throw new UnityException("There is no Facebook Pay Dialog on Windows Phone 8");
        }


        public override void GetAuthResponse(FacebookDelegate callback = null)
        {
            throw new UnityException("There is no Facebook Pay Dialog on Windows Phone 8");
        }
        
        public override void PublishInstall(string appId, FacebookDelegate callback = null) {}

		private IEnumerator GetFBResult(string url, FacebookDelegate fbDelegate) {
			WWW www = new WWW(url);
			yield return www;
			
			if(fbDelegate != null) fbDelegate(new FBResult(www));	
		}

		private IEnumerator LoadSession(){
			if(PlayerPrefs.HasKey("_user_id") && PlayerPrefs.HasKey("_access_token")) {
				
				userId = PlayerPrefs.GetString("_user_id");
				accessToken = PlayerPrefs.GetString("_access_token");
				
				fbClient = new FacebookCS.FacebookClient(accessToken);
				fbClient.AppId = sessionC.AppId; 
				
				string url = "https://graph.facebook.com/me?method=get&access_token=" + accessToken;
				
				WWW www = new WWW(url);
	        	yield return www;
				
				if(www.error != null)
				{
					//Log is true because no internet connection, so assume token is still valid.
					isLoggedIn = true;
				}
				else if(www.text.Contains("first_name")){
					isLoggedIn = true;
				}
				else{ 
					//Token is not valid. Delete session. Set to not logged in;
					DeleteSession();
					isLoggedIn = false;
					userId = "";
					accessToken = "";
					fbClient = null;
				}
	
			}
		}
		
		private void SaveSession(){
			if(isLoggedIn){
				PlayerPrefs.SetString("_user_id", userId);
				PlayerPrefs.SetString("_access_token", accessToken);
			}
		}
		
		private void DeleteSession(){
			try{PlayerPrefs.DeleteKey("_user_id"); } catch{}
			try{PlayerPrefs.DeleteKey("_access_token"); } catch{}
			
		}
		
		
		#region implemented abstract members of Facebook.AbstractFacebook
		public override void GetDeepLink (FacebookDelegate callback)
		{
			Debug.LogError("GetDeepLink not implemented on WP8");
		}

		public override void AppEventsLogEvent (string logEvent, System.Nullable<float> valueToSum, Dictionary<string, object> parameters)
		{
			Debug.LogError("AppEventsLogEvent not implemented on WP8");
		}

		public override void AppEventsLogPurchase (float logPurchase, string currency, Dictionary<string, object> parameters)
		{
			Debug.LogError("AppEventsLogPurchase not implemented on WP8");
		}

		public override bool LimitEventUsage {
			get {
				Debug.LogError("LimitEventUsage not implemented on WP8");
				return false;
			}
			set {
				Debug.LogError("LimitEventUsage not implemented on WP8");
			}
		}
		#endregion
	}
	
}

#endif