using Godot;
public partial class Coin : Area2D
{
	private AnimatedSprite2D _animatedSprite;
	private int value = 1;
	public override void _Ready()
	{
		_animatedSprite = GetNode<AnimatedSprite2D>("Sprite");
		_animatedSprite.Play("idle");
		BodyEntered += OnCoinBodyEntered;
	}

    private void OnCoinBodyEntered(Node2D body)
    {
		CharacterController character = GetNode<CharacterController>(body.GetPath());
		
		if (character == null)
		{
			return;
		}

		character.IncreaseScore(value);
		QueueFree();
    }
}