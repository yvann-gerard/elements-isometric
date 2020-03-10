using UnityEngine;

[System.Serializable]
public class PlayerWeapon
{
	public float cooldown;
	public float shotSpeed;
	public float shotMaxDistance;
	public float shotLifeTime;
	public int maxCharge;
	public int currentcharge;
	public GameObject projectile;
	public bool stopAtMaxDistance;
	public bool unlocked;

	PlayerWeapon(float cooldown, float shotSpeed, float shotMaxDistance, float shotLifeTime, int maxCharge, int currentcharge, GameObject projectile, bool stopAtMaxDistance = false, bool unlocked = false)
	{
		this.cooldown = cooldown;
		this.shotSpeed = shotSpeed;
		this.shotMaxDistance = shotMaxDistance;
		this.shotLifeTime = shotLifeTime;
		this.maxCharge = maxCharge;
		this.currentcharge = currentcharge;
		this.projectile = projectile;
		this.stopAtMaxDistance = stopAtMaxDistance;
		this.unlocked = unlocked;
	}
}
