using Godot;
using System;

public partial class Cadeia : WorldObject
{
	[Export] public bool CadeiaAberta = false;
	[Export] public Character PlayerRef;
	
	
	public override string GetCustomText()
	{
		return "Cadeia";
	}
	
	public override void _Ready()
	{
		base._Ready();
	}
	
	public void EstadoCadeia(bool estado)
	{
		if(estado && !CadeiaAberta)
		{
			CadeiaAberta = estado;
			AbrirCadeia();
		}
		else
		{
			CadeiaAberta = estado;
		}
	}
	
	public void AbrirCadeia()
	{
		if(PlayerRef == null)
		{
			GD.PrintErr("PlayerRef não está configurada! Não é possível mover o personagem.");
			return;
		}
		GD.Print("Cadeia Aberta");
		
		ZIndex = 0;
		
		//Move o Personagem para frente.
		Tween tween = GetTree().CreateTween();
		float novaPosicaoY = -340.0f;
		float duracao = 0.5f;
		tween.TweenProperty(PlayerRef, "global_position:y", novaPosicaoY, duracao);
		
		//Retira as paredes da Cadeia.
		GetTree().Root.GetNode<StaticBody2D>("Node2D/CadeiaParedeEsquerda").QueueFree();
		GetTree().Root.GetNode<StaticBody2D>("Node2D/CadeiaParedeDireita").QueueFree();

	}
	
}
