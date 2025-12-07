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
			GD.Print("------------------------------------------------");
			GD.Print($"Encontrei o nó: {_uiNode.Name}");
			GD.Print($"Tipo nativo: {_uiNode.GetType()}");
			
			var script = _uiNode.GetScript().As<Script>();
			if (script != null)
			{
				GD.Print($"Script anexado: {script.ResourcePath}");
			}
			else
			{
				GD.PrintErr("O nó existe, mas NÃO TEM SCRIPT anexado no jogo rodando!");
			}
			GD.Print("------------------------------------------------");
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
