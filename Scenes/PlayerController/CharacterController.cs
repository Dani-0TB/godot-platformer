using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[Export]
	public float acceleration = 10f;
	public float maxSpeed = 128f;
	public float jumpVelocity = -400.0f;
	public float floorFriction = 10f;
	public float airFriction = 5f;

	private AnimatedSprite2D _sprite;

    public override void _Ready()
    {
		_sprite = GetNode<AnimatedSprite2D>("Sprite");
		_sprite.Play("idle");
    }
    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			if (Input.IsActionJustReleased("jump"))
			{
				velocity.Y /= 4;
			}
		} 

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = jumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X += direction.X * acceleration;
			velocity.X = Mathf.Clamp(velocity.X, -maxSpeed, maxSpeed);
		}
		else
		{	
			if (!IsOnFloor()) 	
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, airFriction);
			} else
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, floorFriction);
			}
		}

		ChangeSpriteDirection();

		Velocity = velocity;
		MoveAndSlide();
	}

	public void ChangeSpriteDirection()
	{
		if (Velocity.X > 0)
		{
			_sprite.FlipH = true;
		}
		else if (Velocity.X < 0)
		{
			_sprite.FlipH = false;
		}

		if(IsOnFloor())
		{
			if (Velocity.X != 0)
			{
				_sprite.Play("walking");
			}
			else 
			{
				_sprite.Play("idle");
			}
		} else
		{
			_sprite.Play("jumping");
		}
	}
}
