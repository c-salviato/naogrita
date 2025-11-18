using Godot;
using System;

public partial class Teste : CharacterBody2D
{
	
	

	//Velocidade Padrao
	public int Speed { get; set; } = 200;
	
		
	private Vector2 _target = Vector2.Zero;
	
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
