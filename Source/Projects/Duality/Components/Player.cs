namespace Duality.Components
{
    public class Player
    {
        public float MovementSpeed;
        public Sprite Sprite;
        public Transform2D Transform => Sprite.Transform;
    }
}
