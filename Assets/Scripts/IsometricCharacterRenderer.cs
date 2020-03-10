using UnityEngine;

public class IsometricCharacterRenderer : MonoBehaviour
{
	string[] staticDirections = { "Static N", "Static NW", "Static W", "Static SW", "Static S", "Static SE", "Static E", "Static NE" };
	string[] runDirections = { "Run N", "Run NW", "Run W", "Run SW", "Run S", "Run SE", "Run E", "Run NE", };
	Animator animator;
	int lastDirectionSlice;

	void Awake()
	{
		animator = GetComponentInChildren<Animator>();
		if(!animator)
			animator = GetComponent<Animator>();
	}

	public void SetDirection(Vector2 direction)
	{
		string[] directionArray = null;

		if (direction.magnitude < .01f) //If we are standing still
			directionArray = staticDirections;
		else
		{
			directionArray = runDirections;
			lastDirectionSlice = DirectionToIndex(direction, staticDirections.Length); //Select the right slice from the current direction
		}
		animator.Play(directionArray[lastDirectionSlice]); //Tell the animator to play the requested state
	}

	//This function converts a Vector2 direction to an index to a slice around a circle
	//This goes in a counter-clockwise direction.
	public static int DirectionToIndex(Vector2 dir, int sliceCount)
	{
		Vector2 normDir = dir.normalized; //Get the normalized direction
		float step = 360f / sliceCount; //Calculate how many degrees one slice is
		float angle = Vector2.SignedAngle(Vector2.up, normDir) + step / 2; //Angle between dir and North offseted so that the north is centerd.
		if (angle < 0)
			angle += 360; //Wrap around the angle.
		float stepCount = angle / step; //get the closest step
		return Mathf.FloorToInt(stepCount); //round the step to get the slice
	}
}