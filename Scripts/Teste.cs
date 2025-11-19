using Godot;
using System;

public partial class Teste : CharacterBody2D
{
	
	

	//Velocidade Padrao
	public int Speed { get; set; } = 200;
	
	private Sprite2D _sprite;
	
	private Vector2 _target = Vector2.Zero;
	
	public override void _Ready()
	{
		_sprite = GetNode<Sprite2D>("Sprite");
		
		if (_sprite == null)
		{
			GD.PushError("O nó Sprite2D não foi encontrado! Verifique o nome.");
		}
	}
	
	//pega a posicao clicada do mouse
	public override void _Input(InputEvent @event)
	{
		if(@event.IsActionPressed("mouse1_click"))
		{
			_target = GetGlobalMousePosition();
		}
	}
	//Processo de Acao
	public override void _PhysicsProcess(double delta)
	{
		if(_sprite != null)
		{
			if((_target.X - GlobalPosition.X) < 0)
			{
				_sprite.FlipH = true;
			}
			else
			{
				_sprite.FlipH = false;
			}
		}
		if(_target == Vector2.Zero)
		{
			return;
		}
		Velocity = GlobalPosition.DirectionTo(_target) * Speed;
		if (GlobalPosition.DistanceTo(_target) > 5)
		{
			MoveAndSlide();
		}
		else
		{
			Velocity = Vector2.Zero;
			_target = Vector2.Zero;
		}
		
	}
}
