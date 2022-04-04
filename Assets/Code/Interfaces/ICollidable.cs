namespace Polar
{
	public interface ICollidable
	{
		void OnCollision();
		void OnDespawn();
		enum ObjectType { None = 0, GoodFood, BadFood, GroundObstacle, AerialObstacle };
		ObjectType GetObjectType();
	}
}
