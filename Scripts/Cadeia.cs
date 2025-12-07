using Godot;
using System;

public partial class Cadeia : WorldObject
{
	[Export] public bool CadeiaAberta = false;
	[Export] public Character PlayerRef;
	private AudioStreamPlayer2D _Abrir;
	
	public override string GetCustomText()
	{
		return "Cadeia";
	}
	public override string DefineSoundEffect()
	{
		return "Keypad";
	}
	public override void _Ready()
	{
		_Abrir = GetNode<AudioStreamPlayer2D>("Abrir");
		base._Ready();
	}
	
	public void PlayOpenSound()
	{
		if(_Abrir != null)
		{
			_Abrir.Play();
		}
		else
		{
			GD.Print("Audio 'Abrir' nao encontrado");
		}
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
		
		PlayOpenSound();
		//Move o Personagem para frente.
		Tween tween = GetTree().CreateTween();
		float novaPosicaoY = 20.0f;
		float duracao = 0.5f;
		tween.TweenProperty(PlayerRef, "global_position:y", novaPosicaoY, duracao);
		
		//Retira as paredes da Cadeia.
		GetTree().Root.GetNode<StaticBody2D>("Node2D/CadeiaParedeEsquerda").QueueFree();
		GetTree().Root.GetNode<StaticBody2D>("Node2D/CadeiaParedeDireita").QueueFree();

	}
	
}
