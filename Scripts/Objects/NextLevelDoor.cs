using Godot;

public partial class NextLevelDoor : Area2D
{

	[Export]
	PackedScene NextLevel;

	public override void _Ready()
	{
		BodyEntered += OnDoorBodyEntered;
	}

	public void OnDoorBodyEntered(Node2D body)
	{
		GetTree().ChangeSceneToPacked(NextLevel);
	}
}
