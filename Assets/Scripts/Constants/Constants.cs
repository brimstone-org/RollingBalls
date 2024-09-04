using System.Collections.Generic;
using UnityEngine;

public static class Constants  {

	public const float FirstStarPercentage = 0.70f;
	public const float SecondStarPercentage = 0.50f;
	public const float ThirdStarPercentage = 0.30f;

    public static readonly Vector3 CameraPositionUp = new Vector3(-1.0f, 0.0f, -10.0f);
    public static readonly Vector3 CameraPositionDown = new Vector3(0.0f, 0.0f, -10.0f);

    public const string SoundPrefSettings = "soundon";

	private static string[] Difficulties = { "easy", "medium", "hard", "extra" };
	public static List<string> Difficulty = new List<string>(Difficulties);

}
