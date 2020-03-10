using Pathfinding;
using UnityEngine;

public class EarthEnnemyController : EnemyController
{
	bool isLava;

	protected new void Update()
	{
		base.Update();
		pathfinder.canMove = !isLava;
		spriteRenderer.color = isLava ? new Color(1, 0, 0) : new Color(0.32f, 0.31f, 0.31f);
	}

	protected new void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if (other.tag.Contains("Shot/Fire"))
			isLava = true;
		else if (other.tag.Contains("Shot/Water"))
			isLava = false;
	}
}