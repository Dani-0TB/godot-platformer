using Godot;
using System;
using System.Reflection;

public partial class Coin : Area2D
{
	private AnimatedSprite2D _animatedSprite;
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play("idle");
		BodyEntered += OnCoinBodyEntered;
	}

    private void OnCoinBodyEntered(Node2D body)
    {
		QueueFree();
    }
}
