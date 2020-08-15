

public struct CollisionInfo {
	public const int NUMBITS = 32;
	public int collideMaskLeft, collideMaskRight, collideMaskBot, collideMaskTop;

	public RaycastCollider2D parentRaycast;

	public bool AnyLeft 	{ get { return collideMaskLeft != 0; } }
	public bool AnyRight 	{ get { return collideMaskRight != 0; }}
	public bool AnyBot 		{ get { return collideMaskBot != 0; }}
	public bool AnyTop 		{ get { return collideMaskTop != 0; }}

	public bool AllLeft 	{ get { return collideMaskLeft == (1<<parentRaycast.horizontalRayCount) - 1; }}
	public bool AllRight 	{ get { return collideMaskRight == (1<<parentRaycast.horizontalRayCount) - 1; }}
	public bool AllTop 		{ get { return collideMaskTop == (1<<parentRaycast.verticalRayCount) - 1; }}
	public bool AllBot 		{ get { return collideMaskBot == (1<<parentRaycast.verticalRayCount) - 1; }}

	public void Init(RaycastCollider2D raycastCollider) {
		parentRaycast = raycastCollider;
		Reset();
	}
	public void Reset() {
		collideMaskBot = collideMaskLeft = collideMaskTop = collideMaskRight = 0;
	}

	public CollisionInfo Or(CollisionInfo other) {
		CollisionInfo info = new CollisionInfo();
		info.Init(parentRaycast);

		info.collideMaskBot = collideMaskBot | other.collideMaskBot;
		info.collideMaskTop = collideMaskTop | other.collideMaskTop;
		info.collideMaskRight = collideMaskRight | other.collideMaskRight;
		info.collideMaskLeft = collideMaskLeft | other.collideMaskLeft;

		return info;
	}

	public bool Any {
		get { return (collideMaskBot | collideMaskLeft | collideMaskRight | collideMaskTop) != 0; }
	}
}
