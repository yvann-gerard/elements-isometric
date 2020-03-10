using UnityEngine;
using UnityEngine.Tilemaps;

public class HideTilemapColliderOnPlay : MonoBehaviour
{
	void Start()
	{
		GetComponent<TilemapRenderer>().enabled = false; //Hides the collider
	}
}