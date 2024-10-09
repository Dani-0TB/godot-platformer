using Godot;
using System;

public partial class Coin : Area2D
{
	private AnimatedSprite2D _animatedSprite;
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play("idle");
		
	}

	public void OnBodyEntered(PhysicsBody2D other)
	{
		QueueFree();
	}
}
