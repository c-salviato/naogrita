using Godot;

public partial class WorldObject : IterateObject
{
	[Export] public PackedScene InspectSceneToLoad;
	
	// Em vez de procurar por texto, vamos referenciar direto
	[Export] public CanvasLayer UiLayerRef; 
	
	public override string GetCustomText()
	{
		return "Caderno";
	}
	public override string DefineSoundEffect()
	{
		return "ClicarCaderno";
	}
	public override void _Ready()
	{
		base._Ready();
		_soundEffect = GetNode<AudioStreamPlayer2D>(DefineSoundEffect());
	}

	public override void Acao()
	{
		OpenInspection();
		PlaySound();
	}

	private void OpenInspection()
	{
		GD.Print("--- Tentando abrir inspeção ---");

		if (InspectSceneToLoad == null)
		{
			GD.PrintErr("ERRO: Você esqueceu de arrastar a cena 'CadeiaPopup.tscn' para o 'InspectSceneToLoad' no Inspector!");
			return;
		}
		
		if (UiLayerRef == null)
		{
			GD.PrintErr("ERRO: Você esqueceu de arrastar a 'UILayer' para o 'UiLayerRef' no Inspector!");
			return;
		}
		
		// Instancia genérico primeiro para testar
		var instance = InspectSceneToLoad.Instantiate();
		
		// Tenta converter para o tipo correto
		var popupInstance = instance as InspectPopupBase;
		
		if (popupInstance == null)
		{
			GD.PrintErr("ERRO DE SCRIPT: A cena que você carregou NÃO tem um script que herda de 'InspectPopupBase'.");
			GD.Print("Tipo real do objeto carregado: " + instance.GetType().ToString());
			instance.QueueFree(); // Limpa da memória já que deu erro
			return;
		}

		// Busca o player
		Character player = GetTree().GetFirstNodeInGroup("Player") as Character;
		
		if (player == null)
		{
			GD.PrintErr("ERRO DE CAMINHO: Não encontrei o Player no caminho 'Node2D/Character'. Verifique a árvore remota.");
			// Dica: Se mudou o nome da cena Main ou do nó do personagem, isso falha.
		}

		if (popupInstance != null && player != null)
		{
			OnMouseExited();
			player.EstadoPopup(true);
			popupInstance.OnClose += () => player.EstadoPopup(false);
			UiLayerRef.AddChild(popupInstance);
			GD.Print("SUCESSO: Popup aberto!");
		}
		else
		{
			GD.Print("FALHA FINAL: Popup ou Player é nulo.");
		}
	}
}
