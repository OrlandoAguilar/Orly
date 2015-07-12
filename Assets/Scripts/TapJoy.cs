using UnityEngine;
using System.Collections;

public class TapJoy : MonoBehaviour {

	string tapPointsLabel = "";
	bool openingFullScreenAd = false;
	static TapJoy instance;
	// Use this for initialization
	void Start () {
		if (FindObjectOfType(typeof(TapJoy))!=this){
			Destroy(this);
		}
		DontDestroyOnLoad(this.gameObject);
		// Set the Handler class. This needs to be a unity GameObject
		TapjoyPlugin.SetCallbackHandler("TapJoy");											// Set this to the name of your linked GameObject 
		TapjoyPlugin.EnableLogging(true);
		// Connect to the Tapjoy servers.
		if (Application.platform == RuntimePlatform.Android)
		{
			TapjoyPlugin.RequestTapjoyConnect(	"08a377a6-e2a3-4a53-8b0a-cd6adf9da299", 				// YOUR APP ID GOES HERE
			                                  "zvlNk6wgO2eOIcwdSzUW");								// YOUR SECRET KEY GOES HERE
		}

		// Get the user virtual currency
		TapjoyPlugin.GetTapPoints();
		
		// Get a banner ad
		TapjoyPlugin.GetDisplayAd();
		TapjoyPlugin.ShowOffers();
		TapjoyPlugin.ShowDisplayAd();
		TapjoyPlugin.SetDisplayAdSize(TapjoyDisplayAdSize.SIZE_320X50);
		TapjoyPlugin.MoveDisplayAd(0,Screen.height-50);
		instance=this;
	}
	
	void Awake()
	{
		// Tapjoy Connect Events
		TapjoyPlugin.connectCallSucceeded += HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed += HandleTapjoyConnectFailed;
		
		// Tapjoy Virtual Currency Events
		TapjoyPlugin.getTapPointsSucceeded += HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed += HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded += HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed += HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded += HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed += HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned += HandleTapPointsEarned;
		
		// Tapjoy Full Screen Ad Events
		TapjoyPlugin.getFullScreenAdSucceeded += HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed += HandleGetFullScreenAdFailed;
		
		// Tapjoy Display Ad Events
		TapjoyPlugin.getDisplayAdSucceeded += HandleGetDisplayAdSucceeded;
		TapjoyPlugin.getDisplayAdFailed += HandleGetDisplayAdFailed;
		
		// Tapjoy Video Ad Events
		TapjoyPlugin.videoAdStarted += HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed += HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted += HandleVideoAdCompleted;
		
		// Tapjoy Ad View Closed Events
		TapjoyPlugin.viewOpened += HandleViewOpened;
		TapjoyPlugin.viewClosed += HandleViewClosed;
		
		// Tapjoy Show Offers Events
		TapjoyPlugin.showOffersFailed += HandleShowOffersFailed;
	}
	
	void OnDisable()
	{
		// Tapjoy Connect Events
		TapjoyPlugin.connectCallSucceeded -= HandleTapjoyConnectSuccess;
		TapjoyPlugin.connectCallFailed -= HandleTapjoyConnectFailed;
		
		// Tapjoy Virtual Currency Events
		TapjoyPlugin.getTapPointsSucceeded -= HandleGetTapPointsSucceeded;
		TapjoyPlugin.getTapPointsFailed -= HandleGetTapPointsFailed;
		TapjoyPlugin.spendTapPointsSucceeded -= HandleSpendTapPointsSucceeded;
		TapjoyPlugin.spendTapPointsFailed -= HandleSpendTapPointsFailed;
		TapjoyPlugin.awardTapPointsSucceeded -= HandleAwardTapPointsSucceeded;
		TapjoyPlugin.awardTapPointsFailed -= HandleAwardTapPointsFailed;
		TapjoyPlugin.tapPointsEarned -= HandleTapPointsEarned;
		
		// Tapjoy Full Screen Ad Events
		TapjoyPlugin.getFullScreenAdSucceeded -= HandleGetFullScreenAdSucceeded;
		TapjoyPlugin.getFullScreenAdFailed -= HandleGetFullScreenAdFailed;
		
		// Tapjoy Display Ad Events
		TapjoyPlugin.getDisplayAdSucceeded -= HandleGetDisplayAdSucceeded;
		TapjoyPlugin.getDisplayAdFailed -= HandleGetDisplayAdFailed;
		
		// Tapjoy Video Ad Events
		TapjoyPlugin.videoAdStarted -= HandleVideoAdStarted;
		TapjoyPlugin.videoAdFailed -= HandleVideoAdFailed;
		TapjoyPlugin.videoAdCompleted -= HandleVideoAdCompleted;
		
		// Tapjoy Ad View Closed Events
		TapjoyPlugin.viewOpened -= HandleViewOpened;
		TapjoyPlugin.viewClosed -= HandleViewClosed;
		
		// Tapjoy Show Offers Events
		TapjoyPlugin.showOffersFailed -= HandleShowOffersFailed;
	}
	
	

	// CONNECT
	public void HandleTapjoyConnectSuccess()
	{
		Debug.Log("C#: HandleTapjoyConnectSuccess");
	}
	
	public void HandleTapjoyConnectFailed()
	{
		Debug.Log("C#: HandleTapjoyConnectFailed");
	}
	
	// VIRTUAL CURRENCY
	void HandleGetTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleGetTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}
	
	public void HandleGetTapPointsFailed()
	{
		Debug.Log("C#: HandleGetTapPointsFailed");
	}
	
	public void HandleSpendTapPointsSucceeded(int points)
	{
		Debug.Log("C#: HandleSpendTapPointsSucceeded: " + points);
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}
	
	public void HandleSpendTapPointsFailed()
	{
		Debug.Log("C#: HandleSpendTapPointsFailed");
	}
	
	public void HandleAwardTapPointsSucceeded()
	{
		Debug.Log("C#: HandleAwardTapPointsSucceeded");
		tapPointsLabel = "Total TapPoints: " + TapjoyPlugin.QueryTapPoints();
	}
	
	public void HandleAwardTapPointsFailed()
	{
		Debug.Log("C#: HandleAwardTapPointsFailed");
	}
	
	public void HandleTapPointsEarned(int points)
	{
		Debug.Log("C#: CurrencyEarned: " + points);
		tapPointsLabel = "Currency Earned: " + points;
		
		TapjoyPlugin.ShowDefaultEarnedCurrencyAlert();
	}
	
	// FULL SCREEN ADS
	public void HandleGetFullScreenAdSucceeded()
	{
		Debug.Log("C#: HandleGetFullScreenAdSucceeded");
		
		TapjoyPlugin.ShowFullScreenAd();
	}
	
	public void HandleGetFullScreenAdFailed()
	{
		Debug.Log("C#: HandleGetFullScreenAdFailed");
	}
	
	// DISPLAY ADS
	public void HandleGetDisplayAdSucceeded()
	{
		Debug.Log("C#: HandleGetDisplayAdSucceeded");
		
		if (!openingFullScreenAd)
			TapjoyPlugin.ShowDisplayAd();
	}
	
	public void HandleGetDisplayAdFailed()
	{
		Debug.Log("C#: HandleGetDisplayAdFailed");
	}
	
	// VIDEO
	public void HandleVideoAdStarted()
	{
		Debug.Log("C#: HandleVideoAdStarted");
	}
	
	public void HandleVideoAdFailed()
	{
		Debug.Log("C#: HandleVideoAdFailed");
	}
	
	public void HandleVideoAdCompleted()
	{
		Debug.Log("C#: HandleVideoAdCompleted");
	}
	
	// VIEW OPENED	
	public void HandleViewOpened(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewOpened of view type " + viewType.ToString());
		openingFullScreenAd = true;
	}
	
	// VIEW CLOSED	
	public void HandleViewClosed(TapjoyViewType viewType)
	{
		Debug.Log("C#: HandleViewClosed of view type " + viewType.ToString());
		openingFullScreenAd = false;
	}
	
	// OFFERS
	public void HandleShowOffersFailed()
	{
		Debug.Log("C#: HandleShowOffersFailed");
	}

	public static void ShowOffers(){
		TapjoyPlugin.ShowOffers();
	}

	public static void getDisplayAdds(){
		TapjoyPlugin.GetDisplayAd();
	}

	public static void getFullScreenAd(){
		TapjoyPlugin.GetFullScreenAd();
	}

	public static void getTapPoints(){
		TapjoyPlugin.GetTapPoints();
	}
}

