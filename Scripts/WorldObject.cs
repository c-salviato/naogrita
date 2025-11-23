using Godot;

public partial class WorldObject : IterateObject
{
	[Export] public PackedScene InspectSceneToLoad;
	
	// Em vez de procurar por texto, vamos referenciar direto
	[Export] public CanvasLayer UiLayerRef; 
	
	public override void _Ready()
	{
		textoCustom = "Caderno";
		base._Ready();
		
		
		InputEvent += OnInputEvent;
	}

	private void OnInputEvent(Node viewport, InputEvent @event, long shapeIdx)
	{
		if (@event is InputEventMouseButton mouseEvent && mouseEvent.Pressed && mouseEvent.ButtonIndex == MouseButton.Left)
		{
			OpenInspection();
		}
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

		var popupInstance = InspectSceneToLoad.Instantiate() as InspectPopupBase;
		
		if (popupInstance != null)
		{
			// Agora usamos a referência que arrastamos
			UiLayerRef.AddChild(popupInstance);
		}
		
		// 2. Encontra o Personagem (usando o nó principal da cena, por exemplo)
		// OBS: O caminho de nó ":/root/Game/Player" é um exemplo. Ajuste conforme sua cena.
		Character player = GetTree().Root.GetNode<Character>("Node2D/Character");
		if (popupInstance != null && player != null)
		{
			// ⭐️ CHAMA O MÉTODO PARA PARAR O MOVIMENTO
			player.EstadoPopup(true);
			
			// 3. Conecta o sinal de fechamento do popup ao método de restauração do movimento.
			popupInstance.OnClose += () => player.EstadoPopup(false);
			// 4. Adiciona à cena
			UiLayerRef.AddChild(popupInstance);
		}
	}
}
