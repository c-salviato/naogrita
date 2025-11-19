using Godot;

public partial class Character : CharacterBody2D
{
	public int Speed { get; set; } = 150;

	private Sprite2D _sprite;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");
		
		if(_sprite == null)
		{
			GD.PushError("Sprite Do Personagem Nao Encontrado");
		}
	}

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
		if(_sprite != null)
		{
			if(Velocity.X == 0)
			{
				return;
			}
			else if(Velocity.X < 0)
			{
				_sprite.FlipH = true;
			}
			else
			{
				_sprite.FlipH = false;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
}
