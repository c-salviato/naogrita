using Godot;
using System;

public partial class MenuInicial : Control
{
	private AudioStreamPlayer MusicaMenu;
	
	public override void _Ready(){
		MusicaMenu = GetNode<AudioStreamPlayer>("MusicaMenu");
		MusicaMenu.Play();
	}
	
	private void OnBotaoIniciarPressed()
	{
		GetTree().ChangeSceneToFile("res://scenes/Main.tscn");
		MusicaMenu.Stop();
	}
}
