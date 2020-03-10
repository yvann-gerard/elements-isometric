using UnityEngine;

public class BasicCameraFollow : MonoBehaviour
{
	public Transform followTarget;
	public float moveSpeed;

	void Update()
	{
		if (followTarget != null)
		{
			Vector3 targetPos = new Vector3(followTarget.position.x, followTarget.position.y, transform.position.z); //Position to go to
			Vector3 velocity = (targetPos - transform.position) * moveSpeed; //Velocity toward targetPos
			transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, 1.0f, Time.deltaTime); //smooth translation to target pos
		}
	}
}