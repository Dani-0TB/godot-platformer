using Godot;
public partial class Ui : Control
{
	// Called when the node enters the scene tree for the first time.
	[Export]
	Label _Score;
	CharacterController _Character;

	// Called once when the object is loaded
	public override void _Ready()
	{
		_Score = GetNode<Label>("Label");
		_Character = GetNode<CharacterController>("../CharacterController");
		_Score.Text = $"Score: {_Character.score}";
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		_Score.Text = $"Score: {_Character.score}";
	}
}
