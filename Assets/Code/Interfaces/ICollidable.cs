namespace Polar
{
	public interface ICollidable
	{
		void OnCollision();
		void OnDespawn();
		enum ObjectType { None, GoodFood, BadFood, GroundObstacle, AerialObstacle };
		ObjectType GetObjectType();
	}
}
