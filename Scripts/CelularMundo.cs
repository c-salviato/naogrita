using Godot;
using System;

public partial class CelularMundo : Area2D
{
	private Node _uiNode; // Mudamos de 'CelularUI' para 'Node' (Genérico)

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
		
		this.InputEvent += OnInputEvent;
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			if (_uiNode != null)
			{
				GD.Print("Tentando abrir o celular via Call()...");
				// Abre a tela ampliada
				_uiNode.Call("ToggleCelular");

				// --- ADICIONE ISSO AQUI EMBAIXO ---
				GD.Print("Celular coletado! Deletando objeto do chão...");
				QueueFree(); // "Fila de Execução para Liberar" -> Destrói o objeto
			}
		}
	}
}
