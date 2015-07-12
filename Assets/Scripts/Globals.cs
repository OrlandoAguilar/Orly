using UnityEngine;
using System.Collections;

public class Globals {
	public static Pinky pinky=null;
	public static Texts texts=new Spanish();
	public static string languaje="Spanish";
	public static bool supportVibration=false;
	public static bool supportsAccelerometer=false;
	public static bool supportTouchScreen=true;
	public static bool supportMouseAndKeyboard=false;
	public static int factorReductorDieds=4;
	public static bool drawControls=true;
	public static bool activatedAccelerometer=false;

	public static int diskItem=1;
	public static int heartItem=1;
	public static int iceItem=1;

	public const int diskItemPrice=60;
	public const int heartItemPrice=85;
	public const int iceItemPrice=82;

	public const int jewelPrice=15;
	public const int coinPrice=10;

	public static int coins=100;
	public static int jewels=3;

	public static int width=800;
	public static int height=600;
}
