using Godot;



public partial class Character : CharacterBody2D
{
	public int Speed { get; set; } = 50;
	[Export] public bool emPopup { get; set; } = false;
	private AudioStreamPlayer2D _movimento;
	private Timer _tempo;
	private Sprite2D _sprite;
	private bool _isMoving = false;

	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");
		_movimento = GetNode<AudioStreamPlayer2D>("Andar");
		_tempo = GetNode<Timer>("tempo");
		
		_tempo.Timeout += OnTempoTimeout;
		if(_movimento == null)
		{
			GD.PushError("Som de Andar Nao Encontrado");
		}
		if(_sprite == null)
		{
			GD.PushError("Sprite Do Personagem Nao Encontrado");
		}
	}
	
	
	
	private void OnTempoTimeout()
	{
		if(_movimento != null)
		{
			_movimento.PitchScale = (float)GD.RandRange(0.9f, 1.1f);
			_movimento.Play();
		}
		else
		{
			GD.Print("Audio 'Andar' nao encontrado");
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
	
	
	public void EstadoPopup(bool estado)
	{
		emPopup = estado;
	}
	
	public override void _PhysicsProcess(double delta)
	{
		if(emPopup)
		{
			return;
		}	
		GetInput();
		
		if(Velocity.LengthSquared() > 0)
		{
			_isMoving = true;
			if (_tempo.IsStopped())
			{
				_tempo.Start();
			}
		}
		else
		{
			_isMoving = false;
			_tempo.Stop();
		}
		MoveAndSlide();
	}
}
