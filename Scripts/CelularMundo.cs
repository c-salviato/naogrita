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
			GD.Print($"[INVESTIGAÇÃO] Encontrei o nó: {_uiNode.Name}");
			GD.Print($"[INVESTIGAÇÃO] Tipo nativo: {_uiNode.GetType()}");
			
			// Vamos ver se tem script anexado de verdade
			var script = _uiNode.GetScript().As<Script>();
			if (script != null)
			{
				GD.Print($"[INVESTIGAÇÃO] Script anexado: {script.ResourcePath}");
			}
			else
			{
				GD.PrintErr("[CRIME DESCOBERTO] O nó existe, mas NÃO TEM SCRIPT anexado no jogo rodando!");
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
				// Remove o nó do CelularMundo (ele ainda está na memória)
				RemoveChild(_soundEffect);
			
				// Adiciona ele à raiz da cena (ou a um nó persistente)
				GetTree().Root.AddChild(_soundEffect);
			
				// Conecta o sinal 'Finished' para que ele se exclua automaticamente
				_soundEffect.Finished += _soundEffect.QueueFree;
				_soundEffect.Play();
			}
			GD.Print("Tentando abrir o celular via Call()...");
			// Abre a tela ampliada
			_uiNode.Call("ToggleCelular");
			// --- ADICIONE ISSO AQUI EMBAIXO ---
			GD.Print("Celular coletado! Deletando objeto do chão...");
			QueueFree(); // "Fila de Execução para Liberar" -> Destrói o objeto
		}
		
	}
}
