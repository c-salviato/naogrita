using Godot;

public partial class Character : CharacterBody2D
{
	public int Speed { get; set; } = 150;

	public void GetInput()
	{

		Vector2 inputDir = new Vector2(0,0);
		if(Input.IsActionPressed("ui_left"))
		{
			inputDir.X = -1;
		}
		if(Input.IsActionPressed("ui_right"))
		{
			inputDir.X = 1;
		}
		Velocity = inputDir * Speed;
	}

	public override void _Process(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
