using Pathfinding;
using UnityEngine;

public class AirEnnemyController : MonoBehaviour
{
	public float directionChangeTime = 3;
	public float movementSpeed = 2;
	Vector2 direction;
	Rigidbody2D rbody;
	int hitCount = 0;
	GameObject fire;
	SpriteRenderer spriteRenderer;
	
	private void Start()
	{
		InvokeRepeating("ChangeDirection", 0, directionChangeTime);
		rbody = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
	}

	private void Update()
	{
		Vector2 newPos = rbody.position + direction * movementSpeed * Time.fixedDeltaTime; //Set new pos toward input
		rbody.MovePosition(newPos);
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		ChangeDirection();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag.Contains("Shot"))
		{
			if (other.tag.Contains("Water"))
				direction = -direction;
			if (other.tag.Contains("Air"))
			{
				if (hitCount > 5)
					Destroy(gameObject);
				movementSpeed *= 0.75f;
				hitCount++;
			}
		}
	}

	void ChangeDirection()
	{
		direction = Random.insideUnitCircle.normalized;
	}
}