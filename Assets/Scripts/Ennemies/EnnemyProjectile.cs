using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyProjectile : MonoBehaviour
{
    void Start()
    {
      StartCoroutine("DelayedDestroy");
    }

		IEnumerator DelayedDestroy()
		{
			yield return new WaitForSeconds(2);
			Destroy(gameObject);
		}
}
