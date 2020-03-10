using MyBox;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(IsometricCharacterRenderer))]
public class EnemyController : MonoBehaviour
{
	protected AIDestinationSetter destinationSetter;
	protected AIPath pathfinder;
	protected Rigidbody2D rbody;
	protected SpriteRenderer spriteRenderer;
	protected bool pathfindingEnabled;
	[SerializeField, ConditionalField("pathfindingEnabled")]
	protected GameObject pathfindingObject;
	[SerializeField]
	protected bool canBeBaited;
	[SerializeField, ConditionalField("canBeBaited")]
	protected string baitTag = "";
	[SerializeField, ConditionalField("canBeBaited")]
	protected float baitRange;
	[SerializeField, ConditionalField("canBeBaited")]
	protected float baitRangeMinimum = 10;
	protected GameObject currentBait;
	protected GameObject staggeredObject;
	Vector2 lastCollidedPosition;
	[SerializeField]
	protected string killerTag = "";
	[SerializeField]
	protected string staggerTag = "";
	IsometricCharacterRenderer isoRenderer;

	#if UNITY_EDITOR
	protected void Reset()
	{
		if(UnityEditor.EditorUtility.DisplayDialog("Choose behavior", "Does this enemy use pathfinding ?", "Yes", "No"))
		{
			destinationSetter = gameObject.AddComponent<AIDestinationSetter>();
			pathfinder = gameObject.AddComponent<AIPath>();
		}
	}
	#endif

	protected void Start() 
	{
		isoRenderer = GetComponent<IsometricCharacterRenderer>();
		rbody = GetComponent<Rigidbody2D>();
		rbody.constraints = RigidbodyConstraints2D.FreezeRotation;
		if(!GetComponentInChildren<SpriteRenderer>())
			spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
		else
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		if(canBeBaited && baitRange > 0)
		{
			CircleCollider2D baitCollider = gameObject.AddComponent<CircleCollider2D>();
			baitCollider.isTrigger = true;
			baitCollider.radius = baitRange;
		}
		if(GetComponent<AIDestinationSetter>())
		{
			destinationSetter = GetComponent<AIDestinationSetter>();
			destinationSetter.target = pathfindingObject.transform;
			pathfinder = GetComponent<AIPath>();
			pathfindingEnabled = true;
		}
	}

	protected void Update()
	{
		isoRenderer.SetDirection(rbody.velocity.normalized);
		if(canBeBaited)
			CheckBait();
		if(staggerTag.Length > 0)
			CheckStagger();
	}

	protected void OnTriggerExit2D(Collider2D other)
	{
		if (other.tag.Contains(baitTag) && (transform.position - other.transform.position).magnitude > baitRangeMinimum) //check if the projectile is the aggro range
			currentBait = null; //Delete the reference to the projectile if it gets deleted
	}

	protected void OnTriggerEnter2D(Collider2D other)
	{
		if (baitTag.Length > 0 && other.tag.Contains(baitTag))
			currentBait = other.gameObject;
		if (killerTag.Length > 0 && other.tag.Contains(killerTag) && (!canBeBaited || (other.transform.position - transform.position).magnitude < baitRangeMinimum))
			Killed();
		if (staggerTag.Length > 0 && other.tag.Contains(staggerTag) && (!canBeBaited || (other.transform.position - transform.position).magnitude < baitRangeMinimum))
			staggeredObject = other.gameObject;
	}

	protected void Killed()
	{
		Destroy(gameObject);
	}

	protected void CheckBait()
	{
			destinationSetter.target = currentBait == null ? pathfindingObject.transform : currentBait.transform; //target bait or the player if there is no bait
	}

	protected void CheckStagger(){
		pathfinder.canMove = staggeredObject == null ? true : false; //disable mouvement if staggered
	}
}
