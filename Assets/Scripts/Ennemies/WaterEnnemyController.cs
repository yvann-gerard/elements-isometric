using UnityEngine;

public class WaterEnnemyController : EnemyController
{
	public GameObject waterCanon;

	protected new void OnTriggerEnter2D(Collider2D other)
	{
		base.OnTriggerEnter2D(other);
		if (other.tag.Contains("Shot/Water"))
		{
			float angle = Vector2.SignedAngle(Vector2.up, transform.position - other.transform.position); //Angle between dir and North offseted so that the north is centerd.
			if (angle < 0)
				angle += 360; //Wrap around the angle.
			for(int i = -20; i <= 20; i+=20)
			{
				GameObject projectile = Instantiate(waterCanon, transform.position, Quaternion.Euler(0, 0, angle + i));
				projectile.GetComponent<Rigidbody2D>().velocity = projectile.transform.up;
			}
			Destroy(gameObject);
		}
	}
}