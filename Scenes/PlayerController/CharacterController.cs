using Godot;
using System;

public partial class CharacterController : CharacterBody2D
{
	[Export]
	public float Speed = 50;
	public float MaxSpeed = 200;
	public float JumpVelocity = -400.0f;
	public float FloorFriction = 50;
	public float AirFriction = 10;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
			if (Input.IsActionJustReleased("jump"))
			{
				velocity.Y /= 2;
			}
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("jump") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X += direction.X * Speed;
			velocity.X = Mathf.Clamp(velocity.X, -MaxSpeed, MaxSpeed);
		}
		else
		{	
			if (!IsOnFloor()) 
			{
				velocity.X = Mathf.MoveToward(Velocity.X, 0, AirFriction);
			} else
			{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, FloorFriction);
			}
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
