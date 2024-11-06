using Godot;
public partial class GenericEnemy : CharacterBody2D
{
	public Vector2 Direction = Vector2.Left;
	public int Speed = 30;

	private RayCast2D _LeftRay;
	private RayCast2D _RightRay;
	private AnimatedSprite2D _Sprite;
	private Area2D _Hitbox;

    public override void _Ready()
    {
		_LeftRay = GetNode<RayCast2D>("LeftRay");
		_RightRay = GetNode<RayCast2D>("RightRay");
		_Sprite = GetNode<AnimatedSprite2D>("Sprite");
		_Sprite.Play("walking");
		_Hitbox = GetNode<Area2D>("Hitbox");
		_Hitbox.BodyEntered += OnHitboxBodyEntered;
    }

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		if (!_LeftRay.IsColliding() || !_RightRay.IsColliding())
		{
			Direction.X *= -1;
			_Sprite.FlipH = !_Sprite.FlipH;
		}

		if (Direction != Vector2.Zero)
		{
			velocity.X = Direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
	private void OnHitboxBodyEntered(Node2D body)
    {
		CharacterController character = GetNode<CharacterController>(body.GetPath());
		
		if (character == null)
		{
			return;
		}

		QueueFree();


    }
}
