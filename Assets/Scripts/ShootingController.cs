using UnityEngine;

public class ShootingController : MonoBehaviour
{
	IsometricCharacterRenderer isoRenderer;
	PlayerWeapon projectile;
	float nextPossibleShotTime;

	public void Initialize(IsometricCharacterRenderer isoRenderer, PlayerWeapon projectile)
	{
		this.isoRenderer = isoRenderer;
		this.projectile = projectile;
		this.nextPossibleShotTime = 0;
	}

	public void Fire(Vector2 position, Vector2 direction)
	{
		if (nextPossibleShotTime <= Time.time && (projectile.maxCharge == 0 || projectile.currentcharge > 0)) //If there is no cooldown and there is the necessary charge
		{
			GameObject currentProjectile = Instantiate(projectile.projectile, position, Quaternion.identity);
			currentProjectile.GetComponent<ProjectileController>().Initialize(direction, projectile);
			nextPossibleShotTime = Time.time + projectile.cooldown; //Set the cooldown time
			projectile.currentcharge = projectile.maxCharge == 0 ? 0 : projectile.currentcharge - 1;
		}
	}

	public void AddCharge(int charge)
	{
		projectile.currentcharge = projectile.currentcharge + charge > projectile.maxCharge ? projectile.maxCharge : projectile.currentcharge + charge; //Add charges up to maximum charge
	}

	public void FullReload()
	{
		projectile.currentcharge = projectile.maxCharge;
	}
}