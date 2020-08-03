using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
public class RaycastCollider2D : MonoBehaviour
{
	const int DIM = 2;

	#region Components

	[HideInInspector] public BoxCollider2D body;

	#endregion

	#region Unity Constants

	[SerializeField] LayerMask collisionMask, platformMask;

	#endregion

		#region Raycasting constants
	public const float skinWidth = .05f;

	[SerializeField, Range(2, 32)]
	public int horizontalRayCount = 4;
	[SerializeField, Range(2, 32)]
	public int verticalRayCount = 4;

	[HideInInspector]
	public float horizontalRaySpacing, verticalRaySpacing;
	#endregion

	#region Raycast Origins
	RaycastOrigin rayCastOrigins;
	#endregion

	#region Collision info 
	[HideInInspector] public CollisionInfo collisionInfo;
	[HideInInspector] public CollisionInfo platformCollisionInfo;
	#endregion

	void Awake() {
		body = GetComponent<BoxCollider2D>();	
		collisionInfo.Init(this);
	}

	void Start() {
		rayCastOrigins.Init();
		rayCastOrigins.PrecomputeRaySpacing(this);
	}

	// faceDir -> -1 if left, 1 if right 
	public bool Move(
		Vector2 velocity, 
		bool platformFallThrough = false, 
		float faceDir = 1
	) {
		collisionInfo.Reset();
		platformCollisionInfo.Reset();
		rayCastOrigins.UpdateRayCastOrigins(this);

		Raycast(ref velocity, platformFallThrough, faceDir);
		// this line is only for ease of writing camera code
		// if (!Mathf.Approximately(velocity.sqrMagnitude, 0))
		transform.Translate(velocity);

		return collisionInfo.Any || platformCollisionInfo.Any;
	}

	void Raycast(ref Vector2 velocity, bool platformFallThrough, float faceDir) {
		for (int dim = 0; dim < 2; dim++) {
			// Useful variables
			float dir = Mathf.Sign(velocity[dim]);
			float rayLength = Mathf.Abs(velocity[dim]) + skinWidth;

			if (faceDir != 0 && dim == 0 && velocity.x == 0) {
				dir = faceDir;
				rayLength = 2 * skinWidth;
			}	
			if (dim == 1 && rayLength <= skinWidth) {
				dir = -1;	
				rayLength = 2 * skinWidth;
			}

			float spacing = dim == 0 ? horizontalRaySpacing : verticalRaySpacing;
			Vector2 dirV = (dim == 0 ? Vector2.right : Vector2.up);
			Vector2 dirVPerp = (dim == 0 ? Vector2.up : Vector2.right);

			if (rayLength > skinWidth) {
				int rayCount = (dim == 0 ? horizontalRayCount : verticalRayCount);
				for (int ray = 0; ray < rayCount; ray++)
				{

					Vector2 rayStart = dim == 0 ? rayCastOrigins.corners[dir == -1 ? 0 : 1, 0] : rayCastOrigins.corners[0, dir == -1 ? 0 : 1];
					rayStart += dirVPerp * (spacing * ray + (dim == 1 ? velocity[dim ^ 1] : 0));

					RaycastHit2D hit = Physics2D.Raycast(rayStart, dirV * dir, rayLength, collisionMask);

					Debug.DrawRay(rayStart, dirV * dir * rayLength, Color.magenta);
					
					if (hit) {
						velocity[dim] = Mathf.Max(0, Mathf.Min(Mathf.Abs(velocity[dim]), (hit.distance - skinWidth))) * dir;
						rayLength = hit.distance;

						// update collisionInfo
						collisionInfo.collideMaskBot |= (dim == 1 && dir < 0 ? 1 : 0) << ray;		
						collisionInfo.collideMaskTop |= (dim == 1 && dir > 0 ? 1 : 0) << ray;		
						collisionInfo.collideMaskLeft |= (dim == 0 && dir < 0 ? 1 : 0) << ray;		
						collisionInfo.collideMaskRight |= (dim == 0 && dir > 0 ? 1 : 0) << ray;		
					} else if (!platformFallThrough) {
						hit = Physics2D.Raycast(rayStart, dirV * dir, rayLength, platformMask);
						if (hit)
						{
							// platform raycast
							if (dir < 0 && dim == 1)
							{
								// if still in platform might as well fall down
								if (hit.distance > skinWidth) {
									velocity[dim] = (hit.distance - skinWidth) * dir;
									rayLength = hit.distance;
								}
							}

							platformCollisionInfo.collideMaskBot |= (dim == 1 && dir < 0 ? 1 : 0) << ray;
							platformCollisionInfo.collideMaskTop |= (dim == 1 && dir > 0 ? 1 : 0) << ray;
							platformCollisionInfo.collideMaskLeft |= (dim == 0 && dir < 0 ? 1 : 0) << ray;
							platformCollisionInfo.collideMaskRight |= (dim == 0 && dir > 0 ? 1 : 0) << ray;
						}
					}
				}
			}
		}
	}
}
