using UnityEngine;

public class MovementController : MonoBehaviour
{
	float movementSpeed;
	IsometricCharacterRenderer isoRenderer;
	Rigidbody2D rbody;

	public void Initialize(IsometricCharacterRenderer isoRenderer, Rigidbody2D rbody, float movementSpeed)
	{
		this.isoRenderer = isoRenderer;
		this.rbody = rbody;
		this.movementSpeed = movementSpeed;
	}

	public void Move(Vector2 direction)
	{
		direction = direction.normalized; //Clamp the magnitude of the inputVector
		isoRenderer.SetDirection(direction); //Set the character's sprite direction
		Vector2 newPos = rbody.position + direction * movementSpeed * Time.fixedDeltaTime; //Set new pos toward input
		rbody.MovePosition(newPos);
	}
}