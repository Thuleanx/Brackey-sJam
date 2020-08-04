using UnityEngine;

public struct RaycastOrigin
{
	const int DIM = 2;

	public Vector2[,] corners;
	public Vector2 Center { get { return (corners[0, 0] + corners[1, 1]) / 2; } }
	public Vector2 bodySizeWithSkin;
	public void Init()
	{
		corners = new Vector2[DIM, DIM];
	}

	public void UpdateRayCastOrigins(RaycastCollider2D collider)
	{
		Bounds bounds = collider.body.bounds;
		bounds.Expand(2 * -RaycastCollider2D.skinWidth);
		for (int i = 0; i < DIM; i++)
			for (int j = 0; j < DIM; j++)
				corners[i, j] = new Vector2(i == 0 ? bounds.min.x : bounds.max.x, j == 0 ? bounds.min.y : bounds.max.y);
	}

	public void PrecomputeRaySpacing(RaycastCollider2D collider)
	{
		Bounds bounds = collider.body.bounds;
		bounds.Expand(2 * -RaycastCollider2D.skinWidth);

		collider.horizontalRaySpacing = Mathf.Clamp(collider.horizontalRaySpacing, 2, int.MaxValue);
		collider.verticalRaySpacing = Mathf.Clamp(collider.verticalRaySpacing, 2, int.MaxValue);

		collider.horizontalRaySpacing = bounds.size.y / (collider.horizontalRayCount - 1);
		collider.verticalRaySpacing = bounds.size.x / (collider.verticalRayCount - 1);

		bounds = collider.body.bounds;
		bodySizeWithSkin = new Vector2(bounds.size.x, bounds.size.y);
	}
}
