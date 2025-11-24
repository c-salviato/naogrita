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
	
	public override void _Ready()
	{
		base._Ready();

	}

	public override void Acao()
	{
		OpenInspection();
	}

	private void OpenInspection()
	{
		// Verificação de segurança
		if (InspectSceneToLoad == null)
		{
			GD.PrintErr("Faltou colocar a Cena do Popup no Inspector!");
			return;
		}
		
		if (UiLayerRef == null)
		{
			GD.PrintErr("Faltou colocar a UILayer no Inspector!");
			return;
		}
		
		OnMouseExited();
		
		var popupInstance = InspectSceneToLoad.Instantiate() as InspectPopupBase;
		
		// 2. Encontra o Personagem (usando o nó principal da cena, por exemplo)
		Character player = GetTree().Root.GetNode<Character>("Node2D/Character");
		if (popupInstance != null && player != null)
		{
			//CHAMA O MÉTODO PARA PARAR O MOVIMENTO
			player.EstadoPopup(true);
			
			// 3. Conecta o sinal de fechamento do popup ao método de restauração do movimento.
			popupInstance.OnClose += () => player.EstadoPopup(false);
			
			UiLayerRef.AddChild(popupInstance);
		}
	}
}
