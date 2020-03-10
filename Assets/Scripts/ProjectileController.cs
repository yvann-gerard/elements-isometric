using UnityEngine;

public class ProjectileController : MonoBehaviour
{
	Rigidbody2D rbody;
	Vector2 direction;
	public Vector2 origin;
	PlayerWeapon projectile;
	public float timeLeft;

	void Start()
	{
		rbody = GetComponent<Rigidbody2D>();
		timeLeft = projectile.shotLifeTime;
	}

	public void Initialize(Vector2 direction, PlayerWeapon projectile)
	{
		origin = transform.position;
		this.projectile = projectile;
		this.direction = direction.normalized; //Get the normalized direction
		float angle = Vector2.SignedAngle(Vector2.up, direction); //Angle between dir and North offseted so that the north is centerd.
		if (angle < 0)
			angle += 360; //Wrap around the angle.
		transform.Rotate(0, 0, angle);
	}

	void Update()
	{
		Vector2 newPos = rbody.position + direction * projectile.shotSpeed * Time.fixedDeltaTime; //Set new pos forward
		rbody.MovePosition(newPos);
		timeLeft -= Time.deltaTime; //Count down the time left
		if (timeLeft <= 0 || Vector2.Distance(origin, transform.position) > projectile.shotMaxDistance && !projectile.stopAtMaxDistance) //if the projectile is too far or past its lifetime
			Destroy(gameObject);
		else if (Vector2.Distance(origin, transform.position) > projectile.shotMaxDistance && projectile.stopAtMaxDistance)
			direction = Vector2.zero;
	}
}
