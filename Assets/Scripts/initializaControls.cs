using UnityEngine;
using System.Collections;

public class initializaControls : MonoBehaviour {
	// Use this for initialization
	void Awake () {
		Globals.supportsAccelerometer =SystemInfo.supportsAccelerometer;
		Globals.activatedAccelerometer=Globals.supportsAccelerometer;

		if (PlayerPrefs.HasKey(GlobalPrefs.activatedAccelerometer) && Globals.activatedAccelerometer)
			Globals.activatedAccelerometer=PlayerPrefs.GetInt(GlobalPrefs.activatedAccelerometer)==1;

		if (PlayerPrefs.HasKey(GlobalPrefs.drawControls))
			Globals.drawControls=PlayerPrefs.GetInt(GlobalPrefs.drawControls)==1;

		if (PlayerPrefs.HasKey(GlobalPrefs.totalJewels))
				Globals.jewels=PlayerPrefs.GetInt(GlobalPrefs.totalJewels);

		if (PlayerPrefs.HasKey(GlobalPrefs.totalCoins))
			Globals.coins=PlayerPrefs.GetInt(GlobalPrefs.totalCoins);

		if (PlayerPrefs.HasKey(GlobalPrefs.totalHeart))
			Globals.heartItem=PlayerPrefs.GetInt(GlobalPrefs.totalHeart);

		if (PlayerPrefs.HasKey(GlobalPrefs.totalIce))
			Globals.iceItem=PlayerPrefs.GetInt(GlobalPrefs.totalIce);

		if (PlayerPrefs.HasKey(GlobalPrefs.totalDisk))
			Globals.diskItem=PlayerPrefs.GetInt(GlobalPrefs.totalDisk);

		Globals.supportVibration =SystemInfo.supportsVibration;
		Globals.supportTouchScreen=Input.multiTouchEnabled;

		#if UNITY_EDITOR || UNITY_WEBPLAYER || UNITY_NACL || UNITY_FLASH || UNITY_WINRT || UNITY_STANDALONE
			Globals.supportMouseAndKeyboard=true;
		#endif
	}
}
