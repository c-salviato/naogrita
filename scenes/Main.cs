using Godot;
using System;

public partial class Main : Node2D
{
	private AudioStreamPlayer MusicaLevel;
	
	public override void _Ready(){
		MusicaLevel = GetNode<AudioStreamPlayer>("MusicaLevel");
		MusicaLevel.Play();
	}
}
