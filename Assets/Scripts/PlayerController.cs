using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float movementSpeed;
	public float cooldown;
	public PlayerWeapon[] ShootingProjectiles;
	public float shotSpeed;
	public float shotMaxDistance;
	public float shotLifeTime;
	public int shotMaxCharge;
	MovementController movementController;
	ShootingController shootingController;

	void Start()
	{
		movementController = gameObject.AddComponent<MovementController>();
		movementController.Initialize(GetComponentInChildren<IsometricCharacterRenderer>(), GetComponent<Rigidbody2D>(), movementSpeed); //Get direction from input

		shootingController = gameObject.AddComponent<ShootingController>();
		shootingController.Initialize(GetComponentInChildren<IsometricCharacterRenderer>(), ShootingProjectiles[0]);
	}

	void Update()
	{
		movementController.Move(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
		if (Input.GetButtonDown("Fire"))
		{
			Vector2 shotDirection = Input.mousePosition;
			shotDirection = Camera.main.ScreenToWorldPoint(shotDirection);
			shotDirection -= (Vector2)transform.position;
			shootingController.Fire(transform.position, shotDirection);
		}
		for (int i = 0; i < 4; i++)
		{
			if (Input.GetButtonDown("SelectWeapon" + (i + 1)) && (ShootingProjectiles[i].unlocked))
				shootingController.Initialize(GetComponentInChildren<IsometricCharacterRenderer>(), ShootingProjectiles[i]);
		}
	}
}