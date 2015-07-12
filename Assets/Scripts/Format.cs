using UnityEngine;
using System.Collections;

public static class Format {

	public static string FormatTime (float time)
	{
		int m = Mathf.FloorToInt (time / 60);
		int s = Mathf.FloorToInt (time % 60);
		int dec = Mathf.FloorToInt((time - Mathf.Floor (time))*10);
		return string.Format ("{0:D2}:{1:D2}:{2:D1}", m, s, dec);
		
	}

	public static int LL2LN(int z){
		return z-3;
	}

	public static int countLevels(){
		return Application.levelCount-3;
	}

	public static int LN2LL(int z){
		return z+3;
	}

}
