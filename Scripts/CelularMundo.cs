using Godot;
using System;

public partial class CelularMundo : WorldObject
{
	private Node _uiNode; // Mudamos de 'CelularUI' para 'Node' (Genérico)
	
	
	
	public override string GetCustomText()
	{
		return "Celular";
	}
	public override string DefineSoundEffect()
	{
		return "PegarCell";
	}
	public override void _Ready()
	{
		_uiNode = GetTree().Root.FindChild("CelularUI", true, false);

		if (_uiNode != null)
		{
			var script = _uiNode.GetScript().As<Script>();
		}
		base._Ready();
	}

	public override void Acao()
	{
		if (_uiNode != null)
		{
			if (_soundEffect != null)
			{
				RemoveChild(_soundEffect);

				GetTree().Root.AddChild(_soundEffect);

				_soundEffect.Finished += _soundEffect.QueueFree;
				_soundEffect.Play();
			}
			GD.Print("Tentando abrir o celular via Call()...");

			_uiNode.Call("ToggleCelular");

			GD.Print("Celular coletado! Deletando objeto do chão...");
			QueueFree();
		}
		
	}
}
