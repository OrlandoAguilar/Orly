using UnityEngine;
using System.Collections;

public class GlobalPrefs {
	public static readonly string totalJewels="TotalJewels";
	public static readonly string totalDisk="TotalDisk";
	public static readonly string totalHeart="TotalHeart";
	public static readonly string totalIce="TotalIce";
	public static readonly string totalCoins="TotalCoins";

	public static readonly string lastLevelPlayed="LastLevelPlayed";
	public static readonly string volumeMusic="VolumeMusic";
	public static readonly string qualityLevel="QualityLevel";
	public static readonly string activatedAccelerometer="ActivatedAccelerometer";

	public static readonly string drawControls="DrawControls";

	public static string getLevelBestTime(int level){
		return string.Format("Level{0}BestTime",level);
	}

	public static string getLevelBestTotal(int level){
		return string.Format("Level{0}BestTotal",level);
	}
///	public const string totalUnlockedLevels="TotalLevels";

}
